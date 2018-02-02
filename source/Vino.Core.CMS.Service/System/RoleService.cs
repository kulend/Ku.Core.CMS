using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.CMS.IService.System;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.CMS.Data.Common;

namespace Vino.Core.CMS.Service.System
{
    public partial class RoleService : BaseService, IRoleService
    {
        protected readonly IRoleRepository _repository;
        protected readonly VinoDbContext _context;
        private readonly IFunctionRepository functionRepository;

        #region 构造函数

        public RoleService(IRoleRepository repository, IFunctionRepository _functionRepository, VinoDbContext context)
        {
            this._repository = repository;
			this.functionRepository = _functionRepository;
            this._context = context;
        }

        #endregion


        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<RoleDto></returns>
        public async Task<List<RoleDto>> GetListAsync(RoleSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<RoleDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<RoleDto> items)> GetListAsync(int page, int size, RoleSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<RoleDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<RoleDto> GetByIdAsync(long id)
        {
            return Mapper.Map<RoleDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(RoleDto dto)
        {
            Role model = Mapper.Map<Role>(dto);
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
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得角色数据！");
                }

                //TODO:这里进行赋值
                item.Name = model.Name;
                item.NameEn = model.NameEn;
                item.IsEnable = model.IsEnable;
                item.Remarks = model.Remarks;

                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        #endregion

        #region 其他方法

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
                var roleFunctions = _context.RoleFunctions.Where(x => x.RoleId == roleId).ToList();
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
            var model = await _context.RoleFunctions.SingleOrDefaultAsync(x => x.RoleId == RoleId && x.FunctionId == FunctionId);
            if (hasAuth)
            {
                if (model == null)
                {
                    model = new RoleFunction();
                    model.RoleId = RoleId;
                    model.FunctionId = FunctionId;
                    await _context.RoleFunctions.AddAsync(model);
                    await _context.SaveChangesAsync();
                }
            }
            else if (model != null)
            {
                _context.RoleFunctions.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #endregion
    }
}
