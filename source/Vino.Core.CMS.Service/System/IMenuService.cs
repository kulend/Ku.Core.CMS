using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Service.System
{
    public interface IMenuService
    {
        MenuDto GetById(long id);

        List<MenuDto> GetMenusByParentId(long parentId);

        void SaveMenu(MenuDto dto);
    }
}
