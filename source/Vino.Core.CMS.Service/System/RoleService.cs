using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System.Partial;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class RoleService : IRoleService
    {
        private IRoleRepository repository;

        public RoleService(IRoleRepository _repository)
        {
            this.repository = _repository;
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
    }
}
