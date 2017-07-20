﻿using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Service.System
{
    public interface IRoleService
    {
        List<RoleDto> GetList(int pageIndex, int pageSize, out int count);

        RoleDto GetById(long id);

        void Save(RoleDto dto);

        void Delete(long id);
    }
}
