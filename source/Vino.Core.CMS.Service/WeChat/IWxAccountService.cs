using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.WeChat;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial interface IWxAccountService
    {
        Task<(int count, List<WxAccountDto> items)> GetListAsync(int page, int size);

        Task<WxAccountDto> GetByIdAsync(long id);

        Task SaveAsync(WxAccountDto dto);

        Task DeleteAsync(long id);
    }
}
