//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：EntityMapperProfile.cs
// 功能描述：AutoMapper初始化
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
// 说明：此代码由工具自动生成，对此文件的更改可能会导致不正确的行为，
// 并且如果重新生成代码，这些更改将会丢失。
//
//----------------------------------------------------------------

using AutoMapper;
using AutoMapper.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ku.Core.CMS.Domain
{
    public class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {
            //UserCenter
            CreateMap<Domain.Entity.UserCenter.User, Domain.Dto.UserCenter.UserDto>().ReverseMap();
            CreateMap<Domain.Entity.UserCenter.Role, Domain.Dto.UserCenter.RoleDto>().ReverseMap();
            CreateMap<Domain.Entity.UserCenter.RoleFunction, Domain.Dto.UserCenter.RoleFunctionDto>().ReverseMap();
            CreateMap<Domain.Entity.UserCenter.UserAddress, Domain.Dto.UserCenter.UserAddressDto>().ReverseMap();
            CreateMap<Domain.Entity.UserCenter.UserPoint, Domain.Dto.UserCenter.UserPointDto>().ReverseMap();
            CreateMap<Domain.Entity.UserCenter.UserPointRecord, Domain.Dto.UserCenter.UserPointRecordDto>().ReverseMap();
			CreateMap<Domain.Entity.UserCenter.UserDialogue, Domain.Dto.UserCenter.UserDialogueDto>().ReverseMap();
            CreateMap<Domain.Entity.UserCenter.UserDialogueMessage, Domain.Dto.UserCenter.UserDialogueMessageDto>().ReverseMap();
            CreateMap<Domain.Entity.System.UserActionLog, Domain.Dto.System.UserActionLogDto>().ReverseMap();
            CreateMap<Domain.Entity.System.Function, Domain.Dto.System.FunctionDto>().ReverseMap();
            CreateMap<Domain.Entity.System.Menu, Domain.Dto.System.MenuDto>().ReverseMap();
            CreateMap<Domain.Entity.System.TimedTask, Domain.Dto.System.TimedTaskDto>().ReverseMap();
            CreateMap<Domain.Entity.Communication.Sms, Domain.Dto.Communication.SmsDto>().ForMember(x => x.Data, opt =>
            {
                opt.ResolveUsing<JsonDeserializeResolver<IDictionary<string, string>>, string>(x => x.Data);
            });
            CreateMap<Domain.Dto.Communication.SmsDto, Domain.Entity.Communication.Sms>().ForMember(x => x.Data, opt =>
            {
                opt.ResolveUsing<JsonSerializeResolver, object>(x => x.Data);
            });
            CreateMap<Domain.Entity.Communication.SmsQueue, Domain.Dto.Communication.SmsQueueDto>().ReverseMap();
            CreateMap<Domain.Entity.Communication.SmsAccount, Domain.Dto.Communication.SmsAccountDto>().ForMember(x => x.ParameterConfig, opt =>
            {
                opt.ResolveUsing<JsonDeserializeResolver<IDictionary<string, string>>, string>(x => x.ParameterConfig);
            });
            CreateMap<Domain.Dto.Communication.SmsAccountDto, Domain.Entity.Communication.SmsAccount>().ForMember(x => x.ParameterConfig, opt =>
            {
                opt.ResolveUsing<JsonSerializeResolver, object>(x => x.ParameterConfig);
            });
            CreateMap<Domain.Entity.Communication.SmsTemplet, Domain.Dto.Communication.SmsTempletDto>().ReverseMap();
            CreateMap<Domain.Entity.System.Notice, Domain.Dto.System.NoticeDto>().ReverseMap();
            CreateMap<Domain.Entity.WeChat.WxAccount, Domain.Dto.WeChat.WxAccountDto>().ReverseMap();
            CreateMap<Domain.Entity.WeChat.WxMenu, Domain.Dto.WeChat.WxMenuDto>().ReverseMap();
            CreateMap<Domain.Entity.WeChat.WxUserTag, Domain.Dto.WeChat.WxUserTagDto>().ReverseMap();
            CreateMap<Domain.Entity.WeChat.WxUser, Domain.Dto.WeChat.WxUserDto>().ReverseMap();
            CreateMap<Domain.Entity.WeChat.WxQrcode, Domain.Dto.WeChat.WxQrcodeDto>().ReverseMap();
            //MaterialCenter
            CreateMap<Domain.Entity.MaterialCenter.UserMaterialGroup, Domain.Dto.MaterialCenter.UserMaterialGroupDto>().ReverseMap();
            CreateMap<Domain.Entity.MaterialCenter.Picture, Domain.Dto.MaterialCenter.PictureDto>().ReverseMap();
            //Content
            CreateMap<Domain.Entity.Content.Advertisement, Domain.Dto.Content.AdvertisementDto>().ReverseMap();
			CreateMap<Domain.Entity.Content.AdvertisementBoard, Domain.Dto.Content.AdvertisementBoardDto>().ReverseMap();
            CreateMap<Domain.Entity.Content.Article, Domain.Dto.Content.ArticleDto>().ReverseMap();
            CreateMap<Domain.Entity.Content.Column, Domain.Dto.Content.ColumnDto>().ReverseMap();
            CreateMap<Domain.Entity.Content.MaskWord, Domain.Dto.Content.MaskWordDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.DeliveryTemplet, Domain.Dto.Mall.DeliveryTempletDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.Payment, Domain.Dto.Mall.PaymentDto>().ForMember(x => x.PaymentConfig, opt =>
            {
                opt.ResolveUsing<JsonDeserializeResolver<IDictionary<string, string>>, string>(x => x.PaymentConfig);
            });
            CreateMap<Domain.Dto.Mall.PaymentDto, Domain.Entity.Mall.Payment>().ForMember(x => x.PaymentConfig, opt =>
            {
                opt.ResolveUsing<JsonSerializeResolver, object>(x => x.PaymentConfig);
            });
            CreateMap<Domain.Entity.Mall.Product, Domain.Dto.Mall.ProductDto>().ForMember(x => x.Properties, opt =>
            {
                opt.ResolveUsing<JsonDeserializeResolver<List<Domain.Dto.Mall.ProductPropertyItem>>, string>(x => x.Properties);
            });
            CreateMap<Domain.Dto.Mall.ProductDto, Domain.Entity.Mall.Product>().ForMember(x => x.Properties, opt =>
            {
                opt.ResolveUsing<JsonSerializeResolver, object>(x => x.Properties);
            });
            CreateMap<Domain.Entity.Mall.ProductSku, Domain.Dto.Mall.ProductSkuDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.ProductCategory, Domain.Dto.Mall.ProductCategoryDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.Brand, Domain.Dto.Mall.BrandDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.BrandCategoryRef, Domain.Dto.Mall.BrandCategoryRefDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.ProductUnit, Domain.Dto.Mall.ProductUnitDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.Order, Domain.Dto.Mall.OrderDto>().ReverseMap();
            CreateMap<Domain.Entity.Mall.OrderProduct, Domain.Dto.Mall.OrderProductDto>().ReverseMap();
            //DataCenter
            CreateMap<Domain.Entity.DataCenter.PageViewRecord, Domain.Dto.DataCenter.PageViewRecordDto>().ReverseMap();
            CreateMap<Domain.Entity.DataCenter.App, Domain.Dto.DataCenter.AppDto>().ReverseMap();
            CreateMap<Domain.Entity.DataCenter.AppVersion, Domain.Dto.DataCenter.AppVersionDto>().ReverseMap();
            CreateMap<Domain.Entity.DataCenter.AppFeedback, Domain.Dto.DataCenter.AppFeedbackDto>().ReverseMap();

            //静态调用初始化
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile(this);
            Mapper.Initialize(cfg);
        }

        private class JsonSerializeResolver : IMemberValueResolver<object, object, object, string>
        {
            public string Resolve(object source, object destination, object sourceMember, string destMember, ResolutionContext context)
            {
                if (sourceMember == null)
                {
                    return null;
                }
                return JsonConvert.SerializeObject(sourceMember);
            }
        }

        private class JsonDeserializeResolver<T> : IMemberValueResolver<object, object, string, T>
        {
            public T Resolve(object source, object destination, string sourceMember, T destMember, ResolutionContext context)
            {
                if (context.Items.ContainsKey("JsonDeserializeIgnore")
                    && (bool)context.Items["JsonDeserializeIgnore"])
                {
                    return default(T);
                }
                if (sourceMember == null)
                {
                    return default(T);
                }
                return JsonConvert.DeserializeObject<T>(sourceMember);
            }
        }
    }
}
