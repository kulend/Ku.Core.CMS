using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.WeChat;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.CMS.IService.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.WeChat.User;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxUserService : BaseService, IWxUserService
    {
        private readonly IWxUserRepository _repository;
        private readonly IWxUserTagRepository wxUserTagRepository;
        private readonly IWxAccountService _wxAccountService;
        private readonly IWcUserTool wcUserTool;
		
        #region 构造函数
		
        public WxUserService(
            IWxUserRepository repository,
            IWxAccountService wxAccountService, 
            IWcUserTool _wcUserTool, 
            IWxUserTagRepository _wxUserTagRepository)
        {
            this._repository = repository;
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
        }

        #endregion
    }
}
