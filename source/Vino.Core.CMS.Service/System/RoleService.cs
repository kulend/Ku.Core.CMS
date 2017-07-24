using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class RoleService : IRoleService
    {
        private VinoDbContext context;
        private IRoleRepository repository;
        private IFunctionRepository functionRepository;

        public RoleService(IRoleRepository _repository, 
            IFunctionRepository _functionRepository,
            VinoDbContext _context)
        {
            this.repository = _repository;
            this.functionRepository = _functionRepository;
            this.context = _context;
        }

        public Task<(int count, List<RoleDto> items)> GetListAsync(int pageIndex, int pageSize)
        {
            (int, List<RoleDto>) Gets()
            {
                int startRow = (pageIndex - 1) * pageSize;
                var queryable = repository.GetQueryable();
                var count = queryable.Count();
                var query = queryable.OrderBy(x => x.CreateTime).Skip(startRow).Take(pageSize);
                return (count, Mapper.Map<List<RoleDto>>(query.ToList()));
            }
            return Task.FromResult(Gets());
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var queryable = repository.GetQueryable();
            var items = Mapper.Map<List<RoleDto>>(await queryable.ToListAsync());
            return items;
        }

        public async Task<RoleDto> GetByIdAsync(long id)
        {
            return Mapper.Map<RoleDto>(await this.repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(RoleDto dto)
        {
            Role model = Mapper.Map<Role>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                await repository.InsertAsync(model);
            }
            else
            {
                //更新
                var role = await repository.GetByIdAsync(model.Id);
                if (role == null)
                {
                    throw new VinoDataNotFoundException("无法取得角色数据!");
                }

                role.Name = model.Name;
                role.NameEn = model.NameEn;
                role.IsEnable = model.IsEnable;
                role.Remarks = model.Remarks;
                repository.Update(role);
            }
            await repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }

        #region 功能权限

        /// <summary>
        /// 取得功能列表（附带角色是否有权限）
        /// </summary>
        public Task<List<FunctionDto>> GetFunctionsWithRoleAuthAsync(long roleId, long? parentFunctionId)
        {
            List<FunctionDto> Gets()
            {
                //取得功能列表
                var queryable = functionRepository.GetQueryable();
                queryable = parentFunctionId.HasValue ? queryable.Where(x => x.ParentId == parentFunctionId.Value) : queryable.Where(x => x.ParentId == null);
                var functions = Mapper.Map<List<FunctionDto>>(queryable.ToList());

                //取得角色功能列表
                var roleFunctions = context.RoleFunctions.Where(x => x.RoleId == roleId).ToList();
                foreach (var function in functions)
                {
                    if (roleFunctions.Any(x => x.FunctionId == function.Id))
                    {
                        function.IsRoleHasAuth = true;
                    }
                }
                return functions;
            }
            return Task.FromResult(Gets());
        }

        public async Task SaveRoleAuthAsync(long RoleId, long FunctionId, bool hasAuth)
        {
            var model = await context.RoleFunctions.SingleOrDefaultAsync(x=>x.RoleId == RoleId && x.FunctionId == FunctionId);
            if (hasAuth)
            {
                if (model == null)
                {
                    model = new RoleFunction();
                    model.RoleId = RoleId;
                    model.FunctionId = FunctionId;
                    await context.RoleFunctions.AddAsync(model);
                    await context.SaveChangesAsync();
                }
            }
            else if(model != null)
            {
                context.RoleFunctions.Remove(model);
                await context.SaveChangesAsync();
            }
        }

        #endregion
    }
}
