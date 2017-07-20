using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Service.System
{
    public interface IUserService
    {
        List<UserDto> GetUserList(int pageIndex, int pageSize, out int count);

        UserDto GetById(long id);

        void SaveUser(UserDto dto);
    }
}
