//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserDialogueService.cs
// 功能描述：用户对话 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-25 10:23
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
{
    public partial class UserDialogueService : BaseService<UserDialogue, UserDialogueDto, UserDialogueSearch>, IUserDialogueService
    {
        #region 构造函数

        public UserDialogueService()
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
        public override async Task<(int count, List<UserDialogueDto> items)> GetListAsync(int page, int size, UserDialogueSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryPageAsync<UserDialogue, User, UserDialogue>(page, size, "t1.*,t2.Id,t2.NickName",
                    $"{dapper.Dialect.FormatTableName<UserDialogue>()} t1 INNER JOIN {dapper.Dialect.FormatTableName<User>()} t2 ON t1.UserId=t2.Id",
                    where.ParseToDapperSql(dapper.Dialect, "t1"), sort as object, (t1, t2) =>
                    {
                        t1.User = t2;
                        return t1;
                    }, splitOn: "Id");
                return (data.count, Mapper.Map<List<UserDialogueDto>>(data.items));
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task AddAsync(long userId, string message)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                //取得对话记录
                var dialogue = await dapper.QueryOneAsync<UserDialogue>(new { UserId = userId, IsDeleted = false});
                if (dialogue != null && dialogue.IsForbidden)
                {
                    //已禁言
                    throw new KuException("您暂时无法留言!");
                }
                if (dialogue == null)
                {
                    //新增对话
                    dialogue = new UserDialogue
                    {
                        Id = ID.NewID(),
                        UserId = userId,
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        LastMessageSummary = message.Substr(0, 120, "..."),
                        LastMessageTime = DateTime.Now,
                        IsForbidden = false,
                        IsSolved = false
                    };

                    await dapper.InsertAsync<UserDialogue>(dialogue);
                }
                else
                {
                    //更新对话信息
                    var item = new
                    {
                        LastMessageSummary = message.Substring(0, 128),
                        LastMessageTime = DateTime.Now,
                        IsSolved = false,
                        SolveTime = null as DateTime?,
                        SolveUserId = null as long?
                    };
                    await dapper.UpdateAsync<UserDialogue>(item, new { dialogue.Id });
                }

                //新增对话消息
                var model = new UserDialogueMessage {
                    Id = ID.NewID(),
                    DialogueId = dialogue.Id,
                    UserId = userId,
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    Message = message,
                    IsAdmin = false,
                };
                await dapper.InsertAsync<UserDialogueMessage>(model);

                dapper.Commit();
            }
        }
    }
}
