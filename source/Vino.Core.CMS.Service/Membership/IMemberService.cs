using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Membership;

namespace Vino.Core.CMS.Service.Membership
{
    public partial interface IMemberService
    {
        Task<(int count, List<MemberDto> items)> GetListAsync(int page, int size);

        Task<MemberDto> GetByIdAsync(long id);

        Task SaveAsync(MemberDto dto);

        Task DeleteAsync(long id);
    }
}
