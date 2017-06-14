using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Entity.System;
using Vino.Core.CMS.Service.System.Dto;

namespace Vino.Core.CMS.Service
{
    public static class VinoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<MenuDto, Menu>();
            });
        }
    }
}
