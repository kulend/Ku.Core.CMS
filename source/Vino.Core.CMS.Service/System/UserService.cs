﻿//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：UserService.cs
// 功能描述：用户 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Service.Events.System;
using Vino.Core.EventBus;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain;
using Ku.Core.Extensions.Dapper;

namespace Vino.Core.CMS.Service.System
{
    public partial class UserService : BaseService, IUserService
    {
        protected readonly IUserRepository _repository;
        private readonly IEventPublisher _eventPublisher;
        protected readonly VinoDbContext context;

        private IDapper _dapper;

        #region 构造函数

        public UserService(IUserRepository repository, IEventPublisher _eventPublisher, VinoDbContext context, IDapper dapper)
        {
            this._repository = repository;
			this._eventPublisher = _eventPublisher;
            this.context = context;
            this._dapper = dapper;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<UserDto></returns>
        public async Task<List<UserDto>> GetListAsync(UserSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<UserDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<UserDto> items)> GetListAsync(int page, int size, UserSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<UserDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<UserDto> GetByIdAsync(long id)
        {
            return Mapper.Map<UserDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(UserDto dto, long[] UserRoleIds)
        {
            User model = Mapper.Map<User>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查账户是否重复
                var cnt = await _repository.GetQueryable().Where(x => x.Account.EqualOrdinalIgnoreCase(model.Account)).CountAsync();
                if (cnt > 0)
                {
                    throw new VinoDataNotFoundException("账户名重复！");
                }
                //检查手机号
                if (model.Mobile.IsNotNullOrEmpty())
                {
                    //格式
                    if (!model.Mobile.IsMobile())
                    {
                        throw new VinoDataNotFoundException("手机号格式不正确！");
                    }
                    //是否重复
                    cnt = await _repository.GetQueryable().Where(x => x.Mobile.EqualOrdinalIgnoreCase(model.Mobile)).CountAsync();
                    if (cnt > 0)
                    {
                        throw new VinoDataNotFoundException("手机号重复！");
                    }
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                var random = new Random();
                model.Factor = random.Next(9999);
                //密码设置
                model.EncryptPassword();
                await _repository.InsertAsync(model);

                //角色处理
                foreach (var roleId in UserRoleIds)
                {
                    UserRole item = new UserRole();
                    item.UserId = model.Id;
                    item.RoleId = roleId;
                    await context.UserRoles.AddAsync(item);
                }
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据！");
                }

                item.Account = model.Account;
                item.Name = model.Name;
                item.Mobile = model.Mobile;
                item.Email = model.Email;
                item.HeadImage = model.HeadImage;
                item.IsEnable = model.IsEnable;
                if (!model.Password.EqualOrdinalIgnoreCase("********************"))
                {
                    item.Password = model.Password;
                    //密码设置
                    item.EncryptPassword();
                }

                item.Remarks = model.Remarks;
                _repository.Update(item);

                //角色处理
                var currentRoles = await context.UserRoles.Where(x => x.UserId == item.Id).ToListAsync();
                foreach (var roleId in UserRoleIds.Where(x => !currentRoles.Any(i => i.RoleId == x)))
                {
                    UserRole ur = new UserRole();
                    ur.UserId = model.Id;
                    ur.RoleId = roleId;
                    await context.UserRoles.AddAsync(ur);
                }
                context.UserRoles.RemoveRange(currentRoles.Where(x => !UserRoleIds.Contains(x.RoleId)));
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

        #region 用户角色

        /// <summary>
        /// 取得用户角色列表
        /// </summary>
        public async Task<List<RoleDto>> GetUserRolesAsync(long userId)
        {
            var queryable = context.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId);
            var items = Mapper.Map<List<RoleDto>>((await queryable.ToListAsync()).Select(x => x.Role));
            return items;
        }

        #endregion

        #region 登陆

        public async Task<UserDto> LoginAsync(string account, string password)
        {
            long id = ID.NewID();
            var aa = await _dapper.InsertAsync(new User {
                Id = id,
                Name = "aa",
                Password = "0",
                Account = "aa"
            });

            var u = await _dapper.QueryOneAsync<User>(new { Id = id });

            u.Name = "AAAAA";
            u.Remarks = "bbb";
            var ab = await _dapper.UpdateAsync(u);

            var cnt = await _dapper.QueryCountAsync<User>(new { Id = id });

            var u2 = await _dapper.QueryOneAsync<User>(new { Name = "AAAAA" });

            var bb = await _dapper.DeleteAsync<User>(new { Id = id });

            if (account.IsNullOrEmpty())
                throw new VinoArgNullException("账户名不能为空！");

            if (password.IsNullOrEmpty())
                throw new VinoArgNullException("密码不能为空！");

            var entity = await context.Users.SingleOrDefaultAsync(x => x.Account.Equals(account,
                StringComparison.OrdinalIgnoreCase));
            if (entity == null || entity.IsDeleted)
            {
                throw new VinoException("账户不存在！");
            }

            if (!entity.CheckPassword(password))
            {
                throw new VinoException("账户或密码出错！");
            }
            if (!entity.IsEnable)
            {
                throw new VinoException("该账号已被禁止登陆！");
            }
            var dto = Mapper.Map<UserDto>(entity);
            dto.Password = "";

            //发送消息
            await _eventPublisher.PublishAsync(new UserLoginEvent { UserId = entity.Id });
            return dto;
        }

        #endregion

        #region 修改密码

        public async Task ChangePassword(long userId, string currentPwd, string newPwd)
        {
            if (currentPwd.IsNullOrEmpty())
            {
                //当前密码不能为空
                throw new VinoArgNullException("当前密码不能为空！");
            }
            if (newPwd.IsNullOrEmpty())
            {
                //新密码不能为空
                throw new VinoArgNullException("新密码不能为空！");
            }
            var item = await _repository.GetByIdAsync(userId);
            if (item == null)
            {
                throw new VinoDataNotFoundException("无法取得用户数据！");
            }

            if (!item.CheckPassword(currentPwd))
            {
                throw new VinoArgNullException("当前密码出错！");
            }
            item.Password = newPwd;
            item.EncryptPassword();
            _repository.Update(item);
            await _repository.SaveAsync();
        }


        #endregion

        #region 验证密码

        public async Task<bool> PasswordCheckAsync(long id, string password)
        {
            if (password.IsNullOrEmpty())
                throw new VinoArgNullException("密码不能为空！");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted)
            {
                throw new VinoException("账户不存在！");
            }
            if (!entity.IsEnable)
            {
                throw new VinoException("该账号已被禁止登陆！");
            }
            return entity.CheckPassword(password);
        }

        #endregion

        #region 用户资料保存

        public async Task SaveProfileAsync(UserDto dto)
        {
            User model = Mapper.Map<User>(dto);

            //更新
            var item = await _repository.GetByIdAsync(model.Id);
            if (item == null)
            {
                throw new VinoDataNotFoundException("无法取得用户数据！");
            }

            item.Name = model.Name;
            item.Mobile = model.Mobile;
            item.HeadImage = model.HeadImage;
            item.Email = model.Email;
            item.Remarks = model.Remarks;
            _repository.Update(item);
            await _repository.SaveAsync();
        }

        #endregion

        #endregion
    }
}
