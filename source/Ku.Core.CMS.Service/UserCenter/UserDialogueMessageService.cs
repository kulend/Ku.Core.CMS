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
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dnc.Extensions.Dapper.Builders;

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
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public override async Task<(int count, List<UserDialogueMessageDto> items)> GetListAsync(int page, int size, UserDialogueMessageSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<UserDialogueMessage>().Concat<User>(u => new { u.Id, u.NickName })
                    .From<UserDialogueMessage>()
                    .InnerJoin<User>().On(new ConditionBuilder().Equal<UserDialogueMessage, User>(m => m.UserId, u => u.Id))
                    .Where(where).Sort(sort as object).Limit(page, size);

                var data = await dapper.QueryPageAsync<UserAddress, User, UserAddress>(builder, (t1, t2) =>
                {
                    t1.User = t2;
                    return t1;
                }, splitOn: "Id");
                return (data.count, Mapper.Map<List<UserDialogueMessageDto>>(data.items));
            }
        }
    }
}
