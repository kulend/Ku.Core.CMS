using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Domain.Entity.Membership;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Helper;

namespace Vino.Core.CMS.Service.Membership
{
    public partial class MemberTypeService
    {
        public async Task<(int count, List<MemberTypeDto> items)> GetListAsync(int page, int size)
        {
            var data = await _repository.PageQueryAsync(page, size, null, "OrderIndex asc");
            return (data.count, _mapper.Map<List<MemberTypeDto>>(data.items));
        }

        public async Task<MemberTypeDto> GetByIdAsync(long id)
        {
            return _mapper.Map<MemberTypeDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(MemberTypeDto dto)
        {
            MemberType model = _mapper.Map<MemberType>(dto);
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
                    throw new VinoDataNotFoundException("无法取得会员类型数据！");
                }

                item.Name = model.Name;
                item.OrderIndex = model.OrderIndex;
                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        public async Task<List<MemberTypeDto>> GetAllAsync()
        {
            var data = await _repository.QueryAsync(null, "OrderIndex asc");
            return  _mapper.Map<List<MemberTypeDto>>(data);
        }
    }
}
