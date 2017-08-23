using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.CMS.Domain;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.System
{
    public partial class FunctionService
    {
        #region 功能模块

        public async Task<(int count, List<FunctionDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize)
        {
            var data = await _repository.PageQueryAsync(pageIndex, pageSize, function => function.ParentId == parentId, "");

            return (data.count, _mapper.Map<List<FunctionDto>>(data.items));
        }

        public async Task SaveAsync(FunctionDto dto)
        {
            Function model = _mapper.Map<Function>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得父功能              
                if (model.ParentId.HasValue)
                {
                    var pModel = await _repository.GetByIdAsync(model.ParentId.Value);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父模块数据!");
                    }
                    if (!pModel.HasSub)
                    {
                        pModel.HasSub = true;
                        _repository.Update(pModel);
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
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var function = await _repository.GetByIdAsync(model.Id);
                if (function == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }

                function.Name = model.Name;
                function.AuthCode = model.AuthCode;
                function.IsEnable = model.IsEnable;
                _repository.Update(function);
            }
            await _repository.SaveAsync();
        }

        public Task<List<FunctionDto>> GetParentsAsync(long parentId)
        {
            List<FunctionDto> Gets()
            {
                return GetParents(parentId);
            }
            return Task.FromResult(Gets());
        }

        public async Task<FunctionDto> GetByIdAsync(long id)
        {
            return _mapper.Map<FunctionDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        private List<FunctionDto> GetParents(long parentId)
        {
            var list = new List<Function>();
            void GetModel(long pid)
            {
                var model = _repository.FirstOrDefault(x => x.Id == pid);
                if (model != null)
                {
                    if (model.ParentId.HasValue)
                    {
                        GetModel(model.ParentId.Value);
                    }
                    list.Add(model);
                }
            }
            GetModel(parentId);
            return _mapper.Map<List<FunctionDto>>(list);
        }

        public async Task<List<FunctionDto>> GetSubsAsync(long? parentId)
        {
            var queryable = _repository.GetQueryable();
            if (parentId.HasValue)
            {
                queryable = queryable.Where(u => u.ParentId == parentId);
            }
            else
            {
                queryable = queryable.Where(u => u.ParentId == null);
            }

            var query = queryable.OrderBy(x => x.CreateTime);
            return _mapper.Map<List<FunctionDto>>(await query.ToListAsync());
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
                var roles = await context.UserRoles.Include(t=>t.Role).Where(x => x.UserId == userId && x.Role.IsEnable).ToListAsync();
                if (roles.Any(x=>x.Role.NameEn.Equals("vino.developer")))
                {
                    authcodes = new List<string> {"vino.develop"};
                }
                else
                {
                    foreach (var role in roles)
                    {
                        var cds = await GetRoleAuthCodes(role.RoleId);
                        codes.AddRange(cds);
                    }
                    //去重
                    authcodes = codes.Distinct().ToList();
                }

                //缓存
                _cache.Add(key, authcodes);
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

        private async Task<List<string>> GetRoleAuthCodes(long roleId)
        {
            var functions = await context.RoleFunctions.Include(t => t.Function)
                .Where(x => x.RoleId == roleId && x.Function.IsEnable).ToListAsync();
            return functions.Select(x => x.Function.AuthCode).ToList();
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
                var roles = await context.UserRoles.Include(t => t.Role).Where(x => x.UserId == userId && x.Role.IsEnable).ToListAsync();
                if (roles.Any(x => x.Role.NameEn.Equals("vino.developer")))
                {
                    authcodes = new List<string> { "vino.develop" };
                }
                else
                {
                    foreach (var role in roles)
                    {
                        var cds = await GetRoleAuthCodes(role.RoleId);
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
            return authcodes;
        }

        #endregion

    }
}
