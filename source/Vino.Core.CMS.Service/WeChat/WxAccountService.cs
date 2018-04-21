//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxAccountService.cs
// 功能描述：公众号 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.WeChat;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.CMS.IService.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.WeChat.AccessToken;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxAccountService : BaseService, IWxAccountService
    {
        protected readonly IWxAccountRepository _repository;
        private readonly IWcAccessTokenTool wcAccessTokenTool;

        #region 构造函数

        public WxAccountService(IWxAccountRepository repository, IWcAccessTokenTool _wcAccessTokenTool)
        {
            this._repository = repository;
            this.wcAccessTokenTool = _wcAccessTokenTool;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<WxAccountDto></returns>
        public async Task<List<WxAccountDto>> GetListAsync(WxAccountSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<WxAccountDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<WxAccountDto> items)> GetListAsync(int page, int size, WxAccountSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<WxAccountDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<WxAccountDto> GetByIdAsync(long id)
        {
            return Mapper.Map<WxAccountDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxAccountDto dto)
        {
            WxAccount model = Mapper.Map<WxAccount>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查AppID
                if (model.AppId.IsNotNullOrEmpty())
                {
                    //是否重复
                    var cnt = await _repository.GetQueryable().Where(x => x.AppId.EqualOrdinalIgnoreCase(model.AppId)).CountAsync();
                    if (cnt > 0)
                    {
                        throw new VinoDataNotFoundException("AppId重复！");
                    }
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得公众号数据！");
                }

                item.Name = model.Name;
                item.OriginalId = model.OriginalId;
                item.Type = model.Type;
                item.WeixinId = model.WeixinId;
                item.Image = model.Image;
                item.AppId = model.AppId;
                item.AppSecret = model.AppSecret;
                item.Token = model.Token;
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

        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        /// <param name="id">公众号ID</param>
        /// <returns></returns>
        public async Task<WcAccessToken> GetAccessToken(long id)
        {
            //取得公众号信息
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
            {
                throw new VinoDataNotFoundException("无法取得公众号数据！");
            }
            if (account.AppId.IsNullOrEmpty() || account.AppSecret.IsNullOrEmpty())
            {
                throw new VinoDataNotFoundException("公众号未设置AppId或AppSecret！");
            }

            var token = await wcAccessTokenTool.GetAsync(account.AppId, account.AppSecret);
            if (token == null || token.Data == null)
            {
                throw new VinoDataNotFoundException("无法取得微信AccessToken，请检查AppId和AppSecret设置是否正确！");
            }
            return token.Data;
        }

        #endregion
    }
}
