using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.WeChat;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.CMS.Domain.Enum;
using Vino.Core.CMS.IService.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.WeChat.AccessToken;
using Vino.Core.WeChat.User;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxUserService : BaseService, IWxUserService
    {
        private readonly ILogger<WxUserService> _logger;
        private readonly IWxUserRepository _repository;
        private readonly IWxUserTagRepository wxUserTagRepository;
        private readonly IWxAccountService _wxAccountService;
        private readonly IWcUserTool wcUserTool;
		
        #region 构造函数
		
        public WxUserService(
            IWxUserRepository repository,
            ILogger<WxUserService> logger,
            IWxAccountService wxAccountService, 
            IWcUserTool _wcUserTool, 
            IWxUserTagRepository _wxUserTagRepository)
        {
            this._repository = repository;
            this._logger = logger;
            this.wxUserTagRepository = _wxUserTagRepository;
            this._wxAccountService = wxAccountService;
            this.wcUserTool = _wcUserTool;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<WxUserDto></returns>
        public async Task<List<WxUserDto>> GetListAsync(WxUserSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<WxUserDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<WxUserDto> items)> GetListAsync(int page, int size, WxUserSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<WxUserDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<WxUserDto> GetByIdAsync(long id)
        {
            return Mapper.Map<WxUserDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(WxUserDto dto)
        {
            WxUser model = Mapper.Map<WxUser>(dto);
            if (model.Id == 0)
            {
                //新增
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
                    throw new VinoDataNotFoundException("无法取得微信用户数据！");
                }

                //TODO:这里进行赋值

                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        #endregion

        #region 其他方法

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

            //取得当前所有标签
            var tagsSerarch = new WxUserTagSearch { AccountId = accountId };
            var localTags = await wxUserTagRepository.QueryAsync(tagsSerarch.GetExpression(), null);
            foreach (var item in localTags)
            {
                //判断是否还有该数据
                var newTag = tags.FirstOrDefault(x=>x.Id == item.TagId);
                if (newTag == null)
                {
                    //远端已删除
                    await wxUserTagRepository.DeleteAsync(item.Id);
                }
                else
                {
                    //更新本地数据
                    if (!item.Name.Equals(newTag.Name) || item.Count != newTag.Count)
                    {
                        item.Name = newTag.Name;
                        item.Count = newTag.Count;
                        wxUserTagRepository.Update(item);
                    }
                    tags.Remove(newTag);
                }
            }

            //保存新的标签
            var newTags = tags.Select(item => new WxUserTag {
                Id = ID.NewID(),
                AccountId = accountId,
                TagId = item.Id,
                Name = item.Name,
                Count = item.Count
            }).ToList();
            await wxUserTagRepository.InsertRangeAsync(newTags);

            //保存
            await wxUserTagRepository.SaveAsync();

            //开始同步用户
            string nextOpenId = null;
            do {
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
                    await SyncOneOpenidAsync(token, accountId, openid);
                }

                nextOpenId = rsp.Data.Data.NextOpenid;
                if (nextOpenId.IsNullOrEmpty())
                {
                    break;
                }

            } while (true);
            //结束同步

            //保存
            await _repository.SaveAsync();
        }

        private async Task SyncOneOpenidAsync(WcAccessToken token, long accountId, string openid)
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
            var user = _repository.FirstOrDefault(x=>x.AccountId == accountId && x.OpenId.Equals(openid));
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
                    await _repository.InsertAsync(user);
                }
            }
            else
            {
                //更新当前信息
                if (wcUser.Subscribe == 1)
                {
                    user.UnionId = wcUser.Unionid;
                    user.IsSubscribe = true;
                    user.NickName = wcUser.Nickname;
                    user.HeadImgUrl = wcUser.Headimgurl;
                    user.Sex = (EmSex)wcUser.Sex;
                    user.Country = wcUser.Country;
                    user.Province = wcUser.Province;
                    user.City = wcUser.City;
                    user.Language = wcUser.Language;
                    user.Remark = wcUser.Remark;
                    user.SubscribeTime = new DateTime(1970, 1, 1).AddSeconds(wcUser.SubscribeTime);
                    user.UserTags = string.Join(",", wcUser.TagidList);
                }
                else
                {
                    user.IsSubscribe = false;
                }

                _repository.Update(user);
            }
        }

        #endregion
    }
}
