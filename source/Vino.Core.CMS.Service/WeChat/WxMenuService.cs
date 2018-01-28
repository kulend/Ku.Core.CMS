using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxMenuService
    {
        public async Task<(int count, List<WxMenuDto> items)> GetListAsync(int page, int size)
        {
            var data = await _repository.PageQueryAsync(page, size, null, "CreateTime asc");
            return (data.count, _mapper.Map<List<WxMenuDto>>(data.items));
        }

        public async Task<WxMenuDto> GetByIdAsync(long id)
        {
            return _mapper.Map<WxMenuDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(WxMenuDto dto)
        {
            WxMenu model = _mapper.Map<WxMenu>(dto);
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
                    throw new VinoDataNotFoundException("无法取得公众号数据！");
                }

                item.Name = model.Name;
                item.MenuData = model.MenuData;
                item.IsIndividuation = model.IsIndividuation;
                item.MatchRule = model.MatchRule;
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
