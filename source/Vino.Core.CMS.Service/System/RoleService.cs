using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.Cache;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public partial class RoleService
    {
        private readonly IFunctionRepository functionRepository;

        public RoleService(VinoDbContext context, ICacheService cache, IMapper mapper, IRoleRepository repository, 
            IFunctionRepository _functionRepository)
            : this(context, cache, mapper, repository)
        {
            this.functionRepository = _functionRepository;
        }

        public async Task<(int count, List<RoleDto> items)> GetListAsync(int pageIndex, int pageSize)
        {
            var data = await _repository.PageQueryAsync(pageIndex, pageSize, null, "");

            return (data.count, _mapper.Map<List<RoleDto>>(data.items));
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var queryable = _repository.GetQueryable();
            var items = _mapper.Map<List<RoleDto>>(await queryable.ToListAsync());
            return items;
        }

        public async Task<RoleDto> GetByIdAsync(long id)
        {
            return _mapper.Map<RoleDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(RoleDto dto)
        {
            Role model = _mapper.Map<Role>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var role = await _repository.GetByIdAsync(model.Id);
                if (role == null)
                {
                    throw new VinoDataNotFoundException("无法取得角色数据!");
                }

                role.Name = model.Name;
                role.NameEn = model.NameEn;
                role.IsEnable = model.IsEnable;
                role.Remarks = model.Remarks;
                _repository.Update(role);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
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
                var functions = _mapper.Map<List<FunctionDto>>(queryable.ToList());

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
