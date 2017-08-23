using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxAccountService
    {
        public async Task<(int count, List<WxAccountDto> items)> GetListAsync(int page, int size)
        {
            var data = await _repository.PageQueryAsync(page, size, null, "CreateTime asc");
            return (data.count, _mapper.Map<List<WxAccountDto>>(data.items));
        }

        public async Task<WxAccountDto> GetByIdAsync(long id)
        {
            return _mapper.Map<WxAccountDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(WxAccountDto dto)
        {
            WxAccount model = _mapper.Map<WxAccount>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查AppID
                if (model.AppId.IsNotNullOrEmpty())
                {
                    //是否重复
                    var cnt = await _repository.GetQueryable().Where(x => x.AppId.EqualOrdinalIgnoreCase(model.AppId)).CountAsync();
                    if (cnt > 0)
                    {
                        throw new VinoDataNotFoundException("AppId重复！");
                    }
                }

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
                item.OriginalId = model.OriginalId;
                item.Type = model.Type;
                item.WeixinId = model.WeixinId;
                item.Image = model.Image;
                item.AppId = model.AppId;
                item.AppSecret = model.AppSecret;
                item.Token = model.Token;
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
