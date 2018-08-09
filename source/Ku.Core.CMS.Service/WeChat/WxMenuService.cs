//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxMenuService.cs
// 功能描述：微信菜单 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.WeChat
{
    public partial class WxMenuService : BaseService<WxMenu, WxMenuDto, WxMenuSearch>, IWxMenuService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxMenuDto dto)
        {
            WxMenu model = Mapper.Map<WxMenu>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                if (!model.IsIndividuation)
                {
                    model.MatchRuleTagId = null;
                    model.MatchRuleSex = null;
                    model.MatchRuleClient = null;
                    model.MatchRuleCountry = null;
                    model.MatchRuleProvince = null;
                    model.MatchRuleCity = null;
                    model.MatchRuleLanguage = null;
                }
                using (var dapper = DapperFactory.Create())
                {
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                ////更新
                //var item = await _repository.GetByIdAsync(model.Id);
                //if (item == null)
                //{
                //    throw new VinoDataNotFoundException("无法取得微信菜单数据！");
                //}

                ////TODO:这里进行赋值
                //item.Name = model.Name;
                //item.MenuData = model.MenuData;
                //item.IsIndividuation = model.IsIndividuation;
                //if (!model.IsIndividuation)
                //{
                //    model.MatchRule = new WxMenuMatchRule
                //    {
                //        TagId = "",
                //        Sex = "",
                //        Client = "",
                //        Country = "",
                //        Province = "",
                //        City = "",
                //        Language = "",
                //    };
                //}

                //if (item.MatchRule != null)
                //{
                //    item.MatchRule.TagId = model.MatchRule.TagId;
                //    item.MatchRule.Sex = model.MatchRule.Sex;
                //    item.MatchRule.Client = model.MatchRule.Client;
                //    item.MatchRule.Country = model.MatchRule.Country;
                //    item.MatchRule.Province = model.MatchRule.Province;
                //    item.MatchRule.City = model.MatchRule.City;
                //    item.MatchRule.Language = model.MatchRule.Language;
                //}
                //else
                //{
                //    item.MatchRule = new WxMenuMatchRule
                //    {
                //        TagId = model.MatchRule.TagId,
                //        Sex = model.MatchRule.Sex,
                //        Client = model.MatchRule.Client,
                //        Country = model.MatchRule.Country,
                //        Province = model.MatchRule.Province,
                //        City = model.MatchRule.City,
                //        Language = model.MatchRule.Language,
                //    };
                //}
                //_repository.Update(item);
            }
        }

    }
}
