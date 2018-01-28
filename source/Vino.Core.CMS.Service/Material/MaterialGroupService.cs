using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Material;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.CMS.Domain.Entity.Material;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Material
{
    public partial class MaterialGroupService
    {
        public async Task<List<MaterialGroupDto>> GetListAsync(long userId)
        {
            var data = await _repository.QueryAsync(x=>x.CreateUserId == userId, x=>x.CreateTime, true);
            return _mapper.Map<List<MaterialGroupDto>>(data);
        }

        public async Task<MaterialGroupDto> GetByIdAsync(long id)
        {
            return _mapper.Map<MaterialGroupDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(MaterialGroupDto dto)
        {
            MaterialGroup model = _mapper.Map<MaterialGroup>(dto);
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
                    throw new VinoDataNotFoundException("无法取得数据！");
                }
                item.Name = model.Name;
                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
