using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Service.System.Dto;

namespace Vino.Core.CMS.Service.System
{
    public interface IMenuService : IDependency
    {
        MenuDto GetById(long id);

        List<MenuDto> GetMenusByParentId(long parentId);

        void SaveMenu(MenuDto dto);
    }
}
