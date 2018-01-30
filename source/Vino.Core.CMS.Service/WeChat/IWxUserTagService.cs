using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.WeChat;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial interface IWxUserTagService
    {
        Task<(int count, List<WxUserTagDto> items)> GetListAsync(int page, int size, WxUserTagSearch where);

        Task<WxUserTagDto> GetByIdAsync(long id);

        Task SaveAsync(WxUserTagDto dto);

        Task DeleteAsync(long id);
    }
}
