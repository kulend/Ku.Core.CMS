//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：FunctionService.cs
// 功能描述：功能 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.Cache;
using Ku.Core.CMS.Data.Common;
using Ku.Core.CMS.Data.Repository.System;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class FunctionService : BaseService<Function, FunctionDto, FunctionSearch>, IFunctionService
    {
        protected readonly ICacheService _cache;

        #region 构造函数

        public FunctionService(ICacheService cache)
        {
            this._cache = cache;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(FunctionDto dto)
        {
            Function model = Mapper.Map<Function>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    //取得父功能              
                    if (model.ParentId.HasValue)
                    {
                        var pModel = await dapper.QueryOneAsync<Function>(new { Id = model.ParentId.Value });
                        if (pModel == null)
                        {
                            throw new VinoDataNotFoundException("无法取得父模块数据!");
                        }
                        if (!pModel.HasSub)
                        {
                            pModel.HasSub = true;

                            await dapper.UpdateAsync<Function>(new { HasSub = true}, new { pModel.Id });
                        }
                        model.Level = pModel.Level + 1;
                    }
                    else
                    {
                        model.ParentId = null;
                        model.Level = 1;
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    await dapper.InsertAsync(model);

                    dapper.Commit();
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new
                    {
                        //这里进行赋值
                        model.Name,
                        model.AuthCode,
                        model.IsEnable,
                    };
                    await dapper.UpdateAsync<Function>(item, new { model.Id });
                }
            }
        }

        #region 其他方法
		
        public async Task<List<FunctionDto>> GetParentsAsync(long parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = new List<Function>();
                async Task GetWhithParentAsync(long pid)
                {
                    var model = await dapper.QueryOneAsync<Function>(new { Id = pid });
                    if (model != null)
                    {
                        if (model.ParentId.HasValue)
                        {
                            await GetWhithParentAsync(model.ParentId.Value);
                        }
                        list.Add(model);
                    }
                }

                await GetWhithParentAsync(parentId);
                return Mapper.Map<List<FunctionDto>>(list);
            }
        }
		
        public async Task<List<FunctionDto>> GetSubsAsync(long? parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = await dapper.QueryListAsync<Function>(new { ParentId = parentId }, new { CreateTime = "asc" });
                return Mapper.Map<List<FunctionDto>>(list);
            }
        }
		
        #endregion
		
        #region 权限认证

        public async Task<bool> CheckUserAuth(long userId, string authCode)
        {
            if (authCode.IsNullOrEmpty())
            {
                return true;
            }
            var key = string.Format(CacheKeyDefinition.UserAuthCode, userId);
            var authcodes  = _cache.Get<List<string>>(key);
            if (authcodes == null)
            {
                //取得用户所有权限码
                List<string> codes = new List<string>();
                //取得所有角色
                IEnumerable<UserRole> roles = null;
                using (var dapper = DapperFactory.Create())
                {
                    var sql = "SELECT t1.*, t2.* FROM system_user_role t1 INNER JOIN system_role t2 ON t1.RoleId=t2.Id AND t2.IsEnable=@IsEnable WHERE t1.UserId=@UserId";
                    roles = await dapper.QueryAsync<UserRole, Role, UserRole>(sql, (t1, t2) =>
                    {
                        t1.Role = t2;
                        return t1;
                    }, param: new { IsEnable= true, UserId = userId }, buffered: true, splitOn:"Id");

                    authcodes = new List<string>();
                    if (roles != null && roles.Any(x => x.Role.NameEn.Equals("vino.developer")))
                    {
                        authcodes.Add("vino.develop");
                    }
                    else if (roles != null)
                    {
                        foreach (var role in roles)
                        {
                            var cds = await GetRoleAuthCodes(dapper, role.RoleId);
                            codes.AddRange(cds);
                        }
                        //去重
                        authcodes = codes.Distinct().ToList();
                    }

                    //缓存
                    _cache.Add(key, authcodes);
                }
            }
            if (authcodes.Contains("vino.develop"))
            {
                return true;
            }
            //验证
            //去除空格
            string tagnobk = authCode.Replace(" ", "");
            //去除括号和操作符
            string tagnofh = tagnobk.Replace("(", ",").Replace(")", ",").Replace("&&", ",").Replace("||", ",");
            //取得所有code
            string[] codeSplits = tagnofh.Split(',');
            if (codeSplits.Length == 1)
            {
                return authcodes.Contains(codeSplits[0]);
            }
            //TODO:以后扩展
            //foreach (var cd in codeSplits)
            //{
            //    if (cd.IsNullOrEmpty())
            //    {
            //        continue;
            //    }

            //    bool auth = authcodes.Contains(cd);
            //    if (auth)
            //    {
            //        Regex reg = new Regex(cd);
            //        tagnobk = reg.Replace(tagnobk, "true", 1);
            //    }
            //    else
            //    {
            //        Regex reg = new Regex(cd);
            //        tagnobk = reg.Replace(tagnobk, "false", 1);
            //    }
            //}

            return authcodes.Contains(authCode);
        }

        private async Task<List<string>> GetRoleAuthCodes(IDapper dapper, long roleId)
        {
            var sql = "SELECT t1.*, t2.* FROM system_role_function t1 INNER JOIN system_function t2 ON t1.FunctionId=t2.Id AND t2.IsEnable=@IsEnable WHERE t1.RoleId=@RoleId";
            var items = await dapper.QueryAsync<RoleFunction, Function, RoleFunction>(sql, (t1, t2) =>
            {
                t1.Function = t2;
                return t1;
            }, param: new { IsEnable = true, RoleId = roleId }, buffered: true, splitOn: "Id");
            return items.Select(x => x.Function.AuthCode).ToList();
        }

        public async Task<List<string>> GetUserAuthCodesAsync(long userId, bool encrypt = false)
        {
            var key = string.Format(encrypt?CacheKeyDefinition.UserAuthCodeEncrypt:CacheKeyDefinition.UserAuthCode, userId);
            var authcodes = _cache.Get<List<string>>(key);
            if (authcodes == null)
            {
                //取得用户所有权限码
                List<string> codes = new List<string>();
                //取得所有角色
                IEnumerable<UserRole> roles = null;
                using (var dapper = DapperFactory.Create())
                {
                    var sql = "SELECT t1.*, t2.* FROM system_user_role t1 INNER JOIN system_role t2 ON t1.RoleId=t2.Id AND t2.IsEnable=@IsEnable WHERE t1.UserId=@UserId";
                    roles = await dapper.QueryAsync<UserRole, Role, UserRole>(sql, (t1, t2) =>
                    {
                        t1.Role = t2;
                        return t1;
                    }, param: new { IsEnable = true, UserId = userId }, buffered: true, splitOn: "Id");

                    authcodes = new List<string>();
                    if (roles != null && roles.Any(x => x.Role.NameEn.Equals("vino.developer")))
                    {
                        authcodes.Add("vino.develop");
                    }
                    else if (roles != null)
                    {
                        foreach (var role in roles)
                        {
                            var cds = await GetRoleAuthCodes(dapper, role.RoleId);
                            codes.AddRange(cds);
                        }
                        //去重
                        authcodes = codes.Distinct().ToList();
                    }
                    if (encrypt)
                    {
                        authcodes = authcodes.Select(CryptHelper.EncryptMD5).ToList();
                    }
                    //缓存
                    _cache.Add(key, authcodes);
                }
            }
            return authcodes;
        }

        #endregion
    }
}
