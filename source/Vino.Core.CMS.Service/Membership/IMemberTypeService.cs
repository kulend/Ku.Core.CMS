using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Membership;

namespace Vino.Core.CMS.Service.Membership
{
    public partial interface IMemberTypeService
    {
        Task<(int count, List<MemberTypeDto> items)> GetListAsync(int page, int size);

        Task<MemberTypeDto> GetByIdAsync(long id);

        Task SaveAsync(MemberTypeDto dto);

        Task DeleteAsync(long id);

        Task<List<MemberTypeDto>> GetAllAsync();
    }
}
