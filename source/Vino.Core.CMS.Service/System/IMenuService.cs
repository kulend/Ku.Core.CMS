using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Service.System
{
    public interface IMenuService
    {
        Task<(int count, List<MenuDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize);

        Task SaveAsync(MenuDto dto);

        Task<List<MenuDto>> GetParentsAsync(long parentId);

        Task<MenuDto> GetByIdAsync(long id);

        Task DeleteAsync(long id);

        Task<List<MenuDto>> GetSubsAsync(long? parentId);
    }
}
