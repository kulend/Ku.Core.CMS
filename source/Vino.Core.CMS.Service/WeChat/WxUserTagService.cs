using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.WeChat;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;
using Vino.Core.CMS.IService.WeChat;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.WeChat.AccessToken;
using Vino.Core.WeChat.User;

namespace Vino.Core.CMS.Service.WeChat
{
    public partial class WxUserTagService : BaseService, IWxUserTagService
    {
        private readonly IWxUserTagRepository _repository;
        private readonly IWcUserTool wcUserTool;
        private readonly IWxAccountService wxAccountService;
		
        #region 构造函数
		
        public WxUserTagService(
            IWxUserTagRepository repository,
            IWxAccountService _wxAccountService,
            IWcUserTool _wcUserTool)
        {
            this._repository = repository;
            this.wcUserTool = _wcUserTool;
            this.wxAccountService = _wxAccountService;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<WxUserTagDto></returns>
        public async Task<List<WxUserTagDto>> GetListAsync(WxUserTagSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<WxUserTagDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<WxUserTagDto> items)> GetListAsync(int page, int size, WxUserTagSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<WxUserTagDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<WxUserTagDto> GetByIdAsync(long id)
        {
            return Mapper.Map<WxUserTagDto>(await this._repository.GetByIdAsync(id));
        }

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
                    throw new VinoDataNotFoundException(rsp.ToString());
                }

                model.TagId = rsp.Data.Tag.Id;

                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得微信用户标签数据！");
                }
                if (item.Name.Equals(model.Name))
                {
                    //没有变动
                    return;
                }

                //TODO:这里进行赋值
                item.Name = model.Name;

                //远程更新
                //取得AccessToken
                var token = await wxAccountService.GetAccessToken(item.AccountId);
                var rsp = await wcUserTool.UpdateUserTag(token, item.TagId, model.Name);
                if (rsp.ErrCode != 0)
                {
                    throw new VinoDataNotFoundException(rsp.ToString());
                }

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
            //取得信息
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                throw new VinoDataNotFoundException("无法取得微信用户标签数据！");
            }
            //远程删除
            //取得AccessToken
            var token = await wxAccountService.GetAccessToken(item.AccountId);
            var rsp = await wcUserTool.DeleteUserTag(token, item.TagId);
            if (rsp.ErrCode != 0)
            {
                throw new VinoDataNotFoundException(rsp.ToString());
            }

            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
