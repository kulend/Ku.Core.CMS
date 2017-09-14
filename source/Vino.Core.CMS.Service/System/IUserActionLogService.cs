using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Service.System
{
    public partial interface IUserActionLogService
    {
        Task AddAsync(UserActionLogDto dto);

        Task<(int count, List<UserActionLogDto> items)> GetListAsync(int page, int size);

        Task<UserActionLogDto> GetByIdAsync(long id);
    }
}
