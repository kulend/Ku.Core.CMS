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
                var data = await dapper.QueryPageAsync<UserDialogueMessage, User, UserDialogueMessage>(page, size, "t1.*,t2.Id,t2.NickName",
                    $"{dapper.Dialect.FormatTableName<UserDialogueMessage>()} t1 INNER JOIN {dapper.Dialect.FormatTableName<User>()} t2 ON t1.UserId=t2.Id",
                    where.ParseToDapperSql(dapper.Dialect, "t1"), sort as object, (t1, t2) =>
                    {
                        t1.User = t2;
                        return t1;
                    }, splitOn: "Id");
                return (data.count, Mapper.Map<List<UserDialogueMessageDto>>(data.items));
            }
        }
    }
}
