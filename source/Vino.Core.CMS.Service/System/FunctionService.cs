using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.Cache;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain;

namespace Vino.Core.CMS.Service.System
{
    public class FunctionService : IFunctionService
    {
        private IFunctionRepository repository;
        private ICacheService cacheService;
        private VinoDbContext context;

        public FunctionService(VinoDbContext _context, 
            IFunctionRepository _repository, ICacheService _cacheService)
        {
            this.repository = _repository;
            this.cacheService = _cacheService;
            this.context = _context;
        }

        #region 功能模块

        public Task<(int count, List<FunctionDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize)
        {
            (int, List<FunctionDto>) Gets()
            {
                int startRow = (pageIndex - 1) * pageSize;
                var queryable = repository.GetQueryable();
                if (parentId.HasValue)
                {
                    queryable = queryable.Where(u => u.ParentId == parentId);
                }
                else
                {
                    queryable = queryable.Where(u => u.ParentId == null);
                }
                var count = queryable.Count();
                var query = queryable.OrderBy(x => x.CreateTime).Skip(startRow).Take(pageSize);
                return (count, Mapper.Map<List<FunctionDto>>(query.ToList()));
            }
            return Task.FromResult(Gets());
        }

        public async Task SaveAsync(FunctionDto dto)
        {
            Function model = Mapper.Map<Function>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得父功能              
                if (model.ParentId.HasValue)
                {
                    var pModel = await repository.GetByIdAsync(model.ParentId.Value);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父模块数据!");
                    }
                    if (!pModel.HasSub)
                    {
                        pModel.HasSub = true;
                        repository.Update(pModel);
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
                await repository.InsertAsync(model);
            }
            else
            {
                //更新
                var function = await repository.GetByIdAsync(model.Id);
                if (function == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }

                function.Name = model.Name;
                function.AuthCode = model.AuthCode;
                function.IsEnable = model.IsEnable;
                repository.Update(function);
            }
            await repository.SaveAsync();
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
            return Mapper.Map<FunctionDto>(await this.repository.GetByIdAsync(id));
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }

        private List<FunctionDto> GetParents(long parentId)
        {
            var list = new List<Function>();
            void GetModel(long pid)
            {
                var model = repository.FirstOrDefault(x => x.Id == pid);
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
            return Mapper.Map<List<FunctionDto>>(list);
        }

        public async Task<List<FunctionDto>> GetSubsAsync(long? parentId)
        {
            var queryable = repository.GetQueryable();
            if (parentId.HasValue)
            {
                queryable = queryable.Where(u => u.ParentId == parentId);
            }
            else
            {
                queryable = queryable.Where(u => u.ParentId == null);
            }

            var query = queryable.OrderBy(x => x.CreateTime);
            return Mapper.Map<List<FunctionDto>>(await query.ToListAsync());
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
            var authcodes  = cacheService.Get<List<string>>(key);
            if (authcodes == null)
            {
                //取得用户所有权限码
                List<string> codes = new List<string>();
                //取得所有角色
                var roles = await context.UserRoles.Include(t=>t.Role).Where(x => x.UserId == userId && x.Role.IsEnable).ToListAsync();
                foreach (var role in roles)
                {
                    var cds = await GetRoleAuthCodes(role.RoleId);
                    codes.AddRange(cds);
                }
                //去重
                authcodes = codes.Distinct().ToList();
                //缓存
                cacheService.Add(key, authcodes);
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

        #endregion

    }
}
