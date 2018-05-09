//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxQrcodeService.cs
// 功能描述：微信二维码 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Common;
using Ku.Core.CMS.Data.Repository.WeChat;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.Domain.Enum.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using Ku.Core.WeChat.Qrcode;

namespace Ku.Core.CMS.Service.WeChat
{
    public partial class WxQrcodeService : BaseService, IWxQrcodeService
    {
        protected readonly IWxQrcodeRepository _repository;
        private readonly IWxAccountService _wxAccountService;
        private readonly IWcQrcodeTool _wcQrcodeTool;

        #region 构造函数

        public WxQrcodeService(IWxQrcodeRepository repository,
            IWxAccountService wxAccountService,
            IWcQrcodeTool wcQrcodeTool
            )
        {
            this._repository = repository;
            this._wxAccountService = wxAccountService;
            this._wcQrcodeTool = wcQrcodeTool;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<WxQrcodeDto></returns>
        public async Task<List<WxQrcodeDto>> GetListAsync(WxQrcodeSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<WxQrcodeDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<WxQrcodeDto> items)> GetListAsync(int page, int size, WxQrcodeSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<WxQrcodeDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<WxQrcodeDto> GetByIdAsync(long id)
        {
            return Mapper.Map<WxQrcodeDto>(await this._repository.GetByIdAsync(id));
        }

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
                var count = await _repository.CountAsync(x => x.AccountId == model.AccountId && x.PeriodType == model.PeriodType);
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

                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得微信二维码数据！");
                }

                //TODO:这里进行赋值
                item.EventKey = model.EventKey;
                item.Purpose = model.Purpose;

                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(params long[] id)
        {
            if (await _repository.DeleteAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task RestoreAsync(params long[] id)
        {
            if (await _repository.RestoreAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
