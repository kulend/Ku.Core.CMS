﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//
// <copyright file="EntityMapperProfile.cs">
//        最后生成时间：2017-08-03 15:06
// </copyright>
//------------------------------------------------------------------------------

using AutoMapper;

namespace Vino.Core.CMS.Web.AutoMapper
{
    public class EntityMapperProfile: Profile
    {
        public EntityMapperProfile()
        {
            CreateMap<Domain.Entity.System.User, Domain.Dto.System.UserDto>();
            CreateMap<Domain.Dto.System.UserDto, Domain.Entity.System.User>();
            CreateMap<Domain.Entity.System.Role, Domain.Dto.System.RoleDto>();
            CreateMap<Domain.Dto.System.RoleDto, Domain.Entity.System.Role>();
            CreateMap<Domain.Entity.System.UserActionLog, Domain.Dto.System.UserActionLogDto>();
            CreateMap<Domain.Dto.System.UserActionLogDto, Domain.Entity.System.UserActionLog>();
            CreateMap<Domain.Entity.System.Function, Domain.Dto.System.FunctionDto>();
            CreateMap<Domain.Dto.System.FunctionDto, Domain.Entity.System.Function>();
            CreateMap<Domain.Entity.System.Menu, Domain.Dto.System.MenuDto>();
            CreateMap<Domain.Dto.System.MenuDto, Domain.Entity.System.Menu>();
            CreateMap<Domain.Entity.Membership.Member, Domain.Dto.Membership.MemberDto>();
            CreateMap<Domain.Dto.Membership.MemberDto, Domain.Entity.Membership.Member>();
            CreateMap<Domain.Entity.Membership.MemberType, Domain.Dto.Membership.MemberTypeDto>();
            CreateMap<Domain.Dto.Membership.MemberTypeDto, Domain.Entity.Membership.MemberType>();
            CreateMap<Domain.Entity.WeChat.WxAccount, Domain.Dto.WeChat.WxAccountDto>();
            CreateMap<Domain.Dto.WeChat.WxAccountDto, Domain.Entity.WeChat.WxAccount>();
        }
    }
}
