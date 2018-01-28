using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.WeChat;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial interface IWxMenuService
    {
        Task<(int count, List<WxMenuDto> items)> GetListAsync(int page, int size);

        Task<WxMenuDto> GetByIdAsync(long id);

        Task SaveAsync(WxMenuDto dto);

        Task DeleteAsync(long id);
    }
}
