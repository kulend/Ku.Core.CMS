using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Material;

namespace Vino.Core.CMS.Service.Material
{
    public partial interface IMaterialGroupService
    {
        Task<List<MaterialGroupDto>> GetListAsync(long userId);

        Task<MaterialGroupDto> GetByIdAsync(long id);

        Task SaveAsync(MaterialGroupDto dto);

        Task DeleteAsync(long id);
    }
}
