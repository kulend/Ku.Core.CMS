//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxQrcodeService.cs
// 功能描述：微信二维码 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.Domain.Enum.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using Ku.Core.WeChat.Qrcode;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.WeChat
{
    public partial class WxQrcodeService : BaseService<WxQrcode, WxQrcodeDto, WxQrcodeSearch>, IWxQrcodeService
    {
        private readonly IWxAccountService _wxAccountService;
        private readonly IWcQrcodeTool _wcQrcodeTool;

        #region 构造函数

        public WxQrcodeService(IWxAccountService wxAccountService,
            IWcQrcodeTool wcQrcodeTool
            )
        {
            this._wxAccountService = wxAccountService;
            this._wcQrcodeTool = wcQrcodeTool;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxQrcodeDto dto)
        {
            WxQrcode model = Mapper.Map<WxQrcode>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;

                //取得新场景值
                using (var dapper = DapperFactory.Create())
                {
                    var count = await dapper.QueryCountAsync<WxQrcode>(new { model.AccountId, model.PeriodType });
                    if (model.PeriodType == EmWxPeriodType.Temp)
                    {
                        model.SceneId = 100000 + count + 1;
                    }
                    else
                    {
                        model.SceneId = count + 1;
                    }

                    //取得微信AccessToken
                    var token = await _wxAccountService.GetAccessToken(model.AccountId);
                    //远程创建
                    if (model.PeriodType == EmWxPeriodType.Temp)
                    {
                        var rsp = await _wcQrcodeTool.CreateTemp(token, model.SceneId, model.ExpireSeconds);
                        if (rsp.ErrCode != 0)
                        {
                            throw new VinoArgNullException(rsp.ToString());
                        }
                        model.Url = rsp.Data.Url;
                        model.Ticket = rsp.Data.Ticket;
                        model.ExpireSeconds = model.ExpireSeconds;
                    }
                    else
                    {
                        var rsp = await _wcQrcodeTool.Create(token, model.SceneId);
                        if (rsp.ErrCode != 0)
                        {
                            throw new VinoArgNullException(rsp.ToString());
                        }
                        model.Url = rsp.Data.Url;
                        model.Ticket = rsp.Data.Ticket;
                    }

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
                        model.EventKey,
                        model.Purpose
                    };
                    await dapper.UpdateAsync<WxQrcode>(item, new { model.Id });
                }
            }
        }
    }
}
