//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxUserService.cs
// 功能描述：微信用户 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.Domain.Enum;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using Ku.Core.WeChat.AccessToken;
using Ku.Core.WeChat.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.WeChat
{
    public partial class WxUserService : BaseService<WxUser, WxUserDto, WxUserSearch>, IWxUserService
    {
        private readonly ILogger<WxUserService> _logger;
        private readonly IWxAccountService _wxAccountService;
        private readonly IWcUserTool wcUserTool;
		
        #region 构造函数
		
        public WxUserService(
            ILogger<WxUserService> logger,
            IWxAccountService wxAccountService, 
            IWcUserTool _wcUserTool)
        {
            this._logger = logger;
            this._wxAccountService = wxAccountService;
            this.wcUserTool = _wcUserTool;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxUserDto dto)
        {
            //WxUser model = Mapper.Map<WxUser>(dto);
            //if (model.Id == 0)
            //{
            //    //新增
            //    model.Id = ID.NewID();
            //    model.CreateTime = DateTime.Now;
            //    model.IsDeleted = false;
            //    await _repository.InsertAsync(model);
            //}
            //else
            //{
            //    //更新
            //    var item = await _repository.GetByIdAsync(model.Id);
            //    if (item == null)
            //    {
            //        throw new VinoDataNotFoundException("无法取得微信用户数据！");
            //    }

            //    //TODO:这里进行赋值

            //    _repository.Update(item);
            //}
            //await _repository.SaveAsync();
        }

        /// <summary>
        /// 保存备注
        /// </summary>
        public async Task SaveRemarkAsync(WxUserDto dto)
        {
            using (var dapper = DapperFactory.Create())
            {
                var item = await dapper.QueryOneAsync<WxUser>(new { Id = dto.Id });
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得微信用户数据！");
                }
                item.Remark = dto.Remark;

                //远程更新备注
                //取得微信AccessToken
                var token = await _wxAccountService.GetAccessToken(item.AccountId);
                var rsp = await wcUserTool.UpdateUserRemark(token, item.OpenId, item.Remark);
                if (rsp.ErrCode != 0)
                {
                    throw new VinoArgNullException(rsp.ToString());
                }

                await dapper.UpdateAsync<WxUser>(new { dto.Remark }, new { dto.Id });
            }
        }

        /// <summary>
        /// 同步数据
        /// </summary>
        public async Task SyncAsync(long accountId)
        {
            //取得微信AccessToken
            var token = await _wxAccountService.GetAccessToken(accountId);

            //开始同步用户标签
            var tagsRsp = await wcUserTool.GetUserTagListAsync(token);
            if (tagsRsp.ErrCode != 0)
            {
                throw new VinoDataNotFoundException(tagsRsp.ToString());
            }
            var tags = tagsRsp.Data.Tags;

            using (var dapper = DapperFactory.CreateWithTrans())
            {
                //取得当前所有标签
                var localTags = await dapper.QueryListAsync<WxUserTag>(new { AccountId = accountId });

                var deltags = new List<long>();
                foreach (var item in localTags)
                {
                    //判断是否还有该数据
                    var newTag = tags.FirstOrDefault(x => x.Id == item.TagId);
                    if (newTag == null)
                    {
                        //远端已删除
                        deltags.Add(item.Id);
                        //await wxUserTagRepository.DeleteAsync(item.Id);
                    }
                    else
                    {
                        //更新本地数据
                        if (!item.Name.Equals(newTag.Name) || item.Count != newTag.Count)
                        {
                            item.Name = newTag.Name;
                            item.Count = newTag.Count;
                            await dapper.UpdateAsync<WxUserTag>(new { newTag.Name, newTag.Count }, new { item.Id });
                        }
                        tags.Remove(newTag);
                    }
                }

                if (deltags.Any())
                {
                    await dapper.DeleteAsync<WxUserTag>(new DapperSql("Id IN @Ids", new { Ids = deltags.ToArray() }));
                }

                //保存新的标签
                var newTags = tags.Select(item => new WxUserTag
                {
                    Id = ID.NewID(),
                    AccountId = accountId,
                    TagId = item.Id,
                    Name = item.Name,
                    Count = item.Count
                });
                await dapper.InsertAsync(newTags);
                //保存
                dapper.Commit();
            }

            using (var dapper = DapperFactory.Create())
            {
                //开始同步用户
                string nextOpenId = null;
                do
                {
                    var rsp = await wcUserTool.GetUserListAsync(token, nextOpenId);
                    if (rsp.ErrCode != 0)
                    {
                        throw new VinoArgNullException(rsp.ToString());
                    }
                    if (rsp.Data.Data.Openid == null
                        || rsp.Data.Data.Openid.Length == 0)
                    {
                        break;
                    }

                    //处理数据
                    foreach (var openid in rsp.Data.Data.Openid)
                    {
                        await SyncOneOpenidAsync(dapper, token, accountId, openid);
                    }

                    nextOpenId = rsp.Data.Data.NextOpenid;
                    if (nextOpenId.IsNullOrEmpty())
                    {
                        break;
                    }

                } while (true);
            }

            //结束同步
        }

        private async Task SyncOneOpenidAsync(IDapper dapper, WcAccessToken token, long accountId, string openid)
        {
            if(openid.IsNullOrEmpty()) return;

            //取得微信用户信息
            var rsp = await wcUserTool.GetUserDetailAsync(token, openid);
            if (rsp.ErrCode != 0)
            {
                _logger.LogError(rsp.ToString());
                return;
            }
            var wcUser = rsp.Data;

            //取得本地用户信息
            var user = await dapper.QueryOneAsync<WxUser>(new { AccountId = accountId, OpenId = openid });
            if (user == null)
            {
                if (wcUser.Subscribe == 1)
                {
                    //创建新用户
                    user = new WxUser
                    {
                        Id = ID.NewID(),
                        AccountId = accountId,
                        OpenId = openid,
                        UnionId = wcUser.Unionid,
                        IsSubscribe = true,
                        NickName = wcUser.Nickname,
                        HeadImgUrl = wcUser.Headimgurl,
                        Sex = (EmSex)wcUser.Sex,
                        Country = wcUser.Country,
                        Province = wcUser.Province,
                        City = wcUser.City,
                        Language = wcUser.Language,
                        Remark = wcUser.Remark,
                        SubscribeTime = new DateTime(1970, 1, 1).AddSeconds(wcUser.SubscribeTime),
                        UserTags = string.Join(",", wcUser.TagidList),
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                    };
                    await dapper.InsertAsync(user);
                }
            }
            else
            {
                //更新当前信息
                if (wcUser.Subscribe == 1)
                {
                    var item = new {
                        UnionId = wcUser.Unionid,
                        IsSubscribe = true,
                        NickName = wcUser.Nickname,
                        HeadImgUrl = wcUser.Headimgurl,
                        Sex = (EmSex)wcUser.Sex,
                        Country = wcUser.Country,
                        Province = wcUser.Province,
                        City = wcUser.City,
                        Language = wcUser.Language,
                        Remark = wcUser.Remark,
                        SubscribeTime = new DateTime(1970, 1, 1).AddSeconds(wcUser.SubscribeTime),
                        UserTags = string.Join(",", wcUser.TagidList),
                    };
                    await dapper.UpdateAsync<WxUser>(item, new { user.Id });
                }
                else
                {
                    await dapper.UpdateAsync<WxUser>(new { IsSubscribe = false }, new { user.Id });
                }
            }
        }
    }
}
