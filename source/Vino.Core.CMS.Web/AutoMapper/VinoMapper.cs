using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Web.AutoMapper
{
    public static class VinoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<MenuDto, Menu>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<FunctionModule, FunctionModuleDto>();
                cfg.CreateMap<FunctionModuleDto, FunctionModule>();
                cfg.CreateMap<FunctionModuleAction, FunctionModuleActionDto>();
                cfg.CreateMap<FunctionModuleActionDto, FunctionModuleAction>();
                cfg.CreateMap<Function, FunctionDto>();
                cfg.CreateMap<FunctionDto, Function>();
            });
        }
    }
}
