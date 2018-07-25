//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserDialogueMessageService.cs
// 功能描述：用户对话消息 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-25 10:24
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
{
    public partial class UserDialogueMessageService : BaseService<UserDialogueMessage, UserDialogueMessageDto, UserDialogueMessageSearch>, IUserDialogueMessageService
    {
        #region 构造函数

        public UserDialogueMessageService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(UserDialogueMessageDto dto)
        {
            UserDialogueMessage model = Mapper.Map<UserDialogueMessage>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
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
                    var item = new {
                        //TODO:这里进行赋值
                    };
                    await dapper.UpdateAsync<UserDialogueMessage>(item, new { model.Id });
                }
            }
        }
    }
}
