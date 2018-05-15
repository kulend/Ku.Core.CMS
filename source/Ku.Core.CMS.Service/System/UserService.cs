//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserService.cs
// 功能描述：用户 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Service.Events.System;
using Ku.Core.EventBus;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class UserService : BaseService<User, UserDto, UserSearch>, IUserService
    {
        private readonly IEventPublisher _eventPublisher;

        #region 构造函数

        public UserService(IEventPublisher eventPublisher)
        {
			_eventPublisher = eventPublisher;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(UserDto dto, long[] UserRoleIds)
        {
            User model = Mapper.Map<User>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.Create())
                {
                    //检查账户是否重复
                    var count = await dapper.QueryCountAsync<User>(new { Account = model.Account });
                    if (count > 0)
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
                        count = await dapper.QueryCountAsync<User>(new { Mobile = model.Mobile });
                        if (count > 0)
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

                    //角色处理
                    var roles = UserRoleIds?.Select(x => new UserRole { UserId = model.Id, RoleId = x });

                    using (var trans = dapper.BeginTrans())
                    {
                        await dapper.InsertAsync(model);

                        await dapper.InsertAsync<UserRole>(roles);

                        trans.Commit();
                    }
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    dynamic item = new ExpandoObject();
                    item.Account = model.Account;
                    item.Name = model.Name;
                    item.Mobile = model.Mobile;
                    item.Email = model.Email;
                    item.HeadImage = model.HeadImage;
                    item.IsEnable = model.IsEnable;
                    item.Remarks = model.Remarks;
                    if (!model.Password.EqualOrdinalIgnoreCase("********************"))
                    {
                        model.EncryptPassword();
                        item.Password = model.Password;
                    }

                    var roles = UserRoleIds?.Select(x => new UserRole { UserId = model.Id, RoleId = x });

                    using (var trans = dapper.BeginTrans())
                    {
                        await dapper.UpdateAsync<User>(item, new { model.Id });

                        //删除所有用户角色数据
                        await dapper.DeleteAsync<UserRole>(new { UserId = model.Id });

                        await dapper.InsertAsync<UserRole>(roles);

                        trans.Commit();
                    }
                }
            }
        }

        #region 其他方法

        #region 用户角色

        /// <summary>
        /// 取得用户角色列表
        /// </summary>
        public async Task<List<RoleDto>> GetUserRolesAsync(long userId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryListAsync<Role>("t1.*", "system_role t1", 
                    where: new DapperSql("EXISTS (SELECT * FROM system_user_role t2 WHERE t2.RoleId=t1.Id AND t2.UserId=@UserId)", new { UserId = userId }), 
                    order: null);
                return Mapper.Map<List<RoleDto>>(data.ToList());
            }
        }

        #endregion

        #region 登陆

        public async Task<UserDto> LoginAsync(string account, string password)
        {
            if (account.IsNullOrEmpty())
                throw new VinoArgNullException("账户名不能为空！");

            if (password.IsNullOrEmpty())
                throw new VinoArgNullException("密码不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                var entity = await _dapper.QueryOneAsync<User>(new { Account = account });
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
            using (var _dapper = DapperFactory.Create())
            {
                var item = await _dapper.QueryOneAsync<User>(new { Id = userId });
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

                await _dapper.UpdateAsync<User>(item);
            }
        }


        #endregion

        #region 验证密码

        public async Task<bool> PasswordCheckAsync(long id, string password)
        {
            if (password.IsNullOrEmpty())
                throw new VinoArgNullException("密码不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                var entity = await _dapper.QueryOneAsync<User>(new { Id = id });
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
        }

        #endregion

        #region 用户资料保存

        /// <summary>
        /// 用户资料保存
        /// </summary>
        public async Task SaveProfileAsync(UserDto dto)
        {
            User model = Mapper.Map<User>(dto);
            using (var _dapper = DapperFactory.Create())
            {
                var item = await _dapper.QueryOneAsync<User>(new { model.Id });
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据！");
                }

                item.Name = model.Name;
                item.Mobile = model.Mobile;
                item.HeadImage = model.HeadImage;
                item.Email = model.Email;
                item.Remarks = model.Remarks;

                await _dapper.UpdateAsync<User>(item);
            }
        }

        #endregion

        #endregion
    }
}
