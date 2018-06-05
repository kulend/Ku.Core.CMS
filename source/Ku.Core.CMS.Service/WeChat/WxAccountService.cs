//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxAccountService.cs
// 功能描述：公众号 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using Ku.Core.WeChat.AccessToken;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.WeChat
{
    public partial class WxAccountService : BaseService<WxAccount, WxAccountDto, WxAccountSearch>, IWxAccountService
    {
        private readonly IWcAccessTokenTool wcAccessTokenTool;

        #region 构造函数

        public WxAccountService(IWcAccessTokenTool _wcAccessTokenTool)
        {
            wcAccessTokenTool = _wcAccessTokenTool;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxAccountDto dto)
        {
            WxAccount model = Mapper.Map<WxAccount>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.Create())
                {
                    //检查AppID
                    if (model.AppId.IsNotNullOrEmpty())
                    {
                        //是否重复
                        var count = await dapper.QueryCountAsync<WxAccount>(new { AppId = model.AppId });
                        if (count > 0)
                        {
                            throw new KuDataNotFoundException("AppId重复！");
                        }
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    model.IsDeleted = false;
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new
                    {
                        //这里进行赋值
                        model.Name,
                        model.OriginalId,
                        model.Type,
                        model.WeixinId,
                        model.Image,
                        model.AppId,
                        model.AppSecret,
                        model.Token,
                    };
                    await dapper.UpdateAsync<WxAccount>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        /// <param name="id">公众号ID</param>
        /// <returns></returns>
        public async Task<WcAccessToken> GetAccessToken(long id)
        {
            //取得公众号信息
            using (var dapper = DapperFactory.Create())
            {
                var account = await dapper.QueryOneAsync<WxAccount>(new { Id = id });
                if (account == null)
                {
                    throw new KuDataNotFoundException("无法取得公众号数据！");
                }
                if (account.AppId.IsNullOrEmpty() || account.AppSecret.IsNullOrEmpty())
                {
                    throw new KuDataNotFoundException("公众号未设置AppId或AppSecret！");
                }

                var token = await wcAccessTokenTool.GetAsync(account.AppId, account.AppSecret);
                if (token == null || token.Data == null)
                {
                    throw new KuDataNotFoundException("无法取得微信AccessToken，请检查AppId和AppSecret设置是否正确！");
                }
                return token.Data;
            }
        }
    }
}
