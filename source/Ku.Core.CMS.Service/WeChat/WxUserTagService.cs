//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxUserTagService.cs
// 功能描述：微信用户标签 业务逻辑处理类
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
using Ku.Core.WeChat.User;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.WeChat
{
    public partial class WxUserTagService : BaseService<WxUserTag, WxUserTagDto, WxUserTagSearch>, IWxUserTagService
    {
        private readonly IWcUserTool wcUserTool;
        private readonly IWxAccountService wxAccountService;
		
        #region 构造函数
		
        public WxUserTagService(
            IWxAccountService _wxAccountService,
            IWcUserTool _wcUserTool)
        {
            wcUserTool = _wcUserTool;
            wxAccountService = _wxAccountService;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxUserTagDto dto)
        {
            WxUserTag model = Mapper.Map<WxUserTag>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.Count = 0;
                model.CreateTime = DateTime.Now;

                //远程添加

                //取得AccessToken
                var token = await wxAccountService.GetAccessToken(model.AccountId);
                var rsp = await wcUserTool.CreateUserTag(token, model.Name);
                if (rsp.ErrCode != 0)
                {
                    throw new KuDataNotFoundException(rsp.ToString());
                }

                model.TagId = rsp.Data.Tag.Id;

                using (var dapper = DapperFactory.Create())
                {
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = await dapper.QueryOneAsync<WxUserTag>(new { model.Id });
                    if (item == null)
                    {
                        throw new KuDataNotFoundException("无法取得微信用户标签数据！");
                    }
                    if (item.Name.Equals(model.Name))
                    {
                        //没有变动
                        return;
                    }
                    //远程更新
                    //取得AccessToken
                    var token = await wxAccountService.GetAccessToken(item.AccountId);
                    var rsp = await wcUserTool.UpdateUserTag(token, item.TagId, model.Name);
                    if (rsp.ErrCode != 0)
                    {
                        throw new KuDataNotFoundException(rsp.ToString());
                    }

                    await dapper.UpdateAsync<WxUserTag>(new { model.Name }, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public override async Task DeleteAsync(params long[] id)
        {
            using (var dapper = DapperFactory.Create())
            {
                foreach (var i in id)
                {
                    //取得信息
                    var item = await dapper.QueryOneAsync<WxUserTag>(new { Id = id });
                    if (item == null)
                    {
                        continue;
                    }

                    //远程删除
                    //取得AccessToken
                    var token = await wxAccountService.GetAccessToken(item.AccountId);
                    var rsp = await wcUserTool.DeleteUserTag(token, item.TagId);
                    if (rsp.ErrCode != 0)
                    {
                        throw new KuDataNotFoundException(rsp.ToString());
                    }
                    await dapper.DeleteAsync<WxUserTag>(new { Id = id });
                }
            }

        }
    }
}
