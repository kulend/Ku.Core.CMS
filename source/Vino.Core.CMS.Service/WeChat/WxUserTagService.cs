using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxUserTagService
    {
        public async Task<(int count, List<WxUserTagDto> items)> GetListAsync(int page, int size, WxUserTagSearch where)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), "TagId asc");
            return (data.count, _mapper.Map<List<WxUserTagDto>>(data.items));
        }

        public async Task<WxUserTagDto> GetByIdAsync(long id)
        {
            return _mapper.Map<WxUserTagDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(WxUserTagDto dto)
        {
            WxUserTag model = _mapper.Map<WxUserTag>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得公众号数据！");
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
