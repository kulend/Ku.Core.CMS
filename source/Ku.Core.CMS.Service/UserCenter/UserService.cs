//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserService.cs
// 功能描述：用户 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 10:45
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.EventBus;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Extensions.Dapper.Builders;
using Ku.Core.CMS.Domain.Enum.UserCenter;

namespace Ku.Core.CMS.Service.UserCenter
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
        public async Task SaveAsync(UserDto dto, long[] userRoles)
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
                        throw new KuException("账户名重复！");
                    }

                    //检查手机号
                    if (model.Mobile.IsNotNullOrEmpty())
                    {
                        //格式
                        if (!model.Mobile.IsMobile())
                        {
                            throw new KuException("手机号格式不正确！");
                        }
                        //是否重复
                        count = await dapper.QueryCountAsync<User>(new { Mobile = model.Mobile });
                        if (count > 0)
                        {
                            throw new KuException("手机号重复！");
                        }
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    model.IsDeleted = false;
                    model.Factor = new Random().Next(9999);
                    //密码加密处理
                    model.EncryptPassword();

                    if (!model.IsAdmin)
                    {
                        userRoles = new long[] { };
                    }
                    //角色处理
                    var roles = userRoles?.Select(x => new UserRole { UserId = model.Id, RoleId = x });

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
                    var user = await dapper.QueryOneAsync<User>(new { model.Id});
                    if (user == null)
                    {
                        throw new KuDataNotFoundException("无法取得用户数据！");
                    }
                    if (!user.Account.Equals(model.Account))
                    {
                        //检查账户是否重复
                        var count = await dapper.QueryCountAsync<User>(new { Account = model.Account });
                        if (count > 0)
                        {
                            throw new KuException("账户名重复！");
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Mobile) && !model.Mobile.Equals(user.Mobile))
                    {
                        //检查手机号
                        //格式
                        if (!model.Mobile.IsMobile())
                        {
                            throw new KuException("手机号格式不正确！");
                        }
                        //是否重复
                        var count = await dapper.QueryCountAsync<User>(new { Mobile = model.Mobile });
                        if (count > 0)
                        {
                            throw new KuException("手机号重复！");
                        }
                    }
                    dynamic item = new ExpandoObject();
                    item.Account = model.Account;
                    item.NickName = model.NickName;
                    item.RealName = model.RealName;
                    item.Mobile = model.Mobile;
                    item.Email = model.Email;
                    item.Sex = model.Sex;
                    item.HeadImage = model.HeadImage;
                    item.IsEnable = model.IsEnable;
                    item.Remarks = model.Remarks;
                    item.IsAdmin = model.IsAdmin;
                    item.QQ = model.QQ;
                    item.WebsiteUrl = model.WebsiteUrl;
                    item.Signature = model.Signature;
                    if (!model.Password.EqualOrdinalIgnoreCase("********************"))
                    {
                        model.EncryptPassword();
                        item.Password = model.Password;
                    }
                    if (!model.IsAdmin)
                    {
                        userRoles = new long[] { };
                    }
                    var roles = userRoles?.Select(x => new UserRole { UserId = model.Id, RoleId = x });

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

        /// <summary>
        /// 取得用户角色列表
        /// </summary>
        public async Task<List<RoleDto>> GetUserRolesAsync(long userId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<Role>().From<Role>().Where(new RoleSearch { UserId = userId });
                var data = await dapper.QueryListAsync<Role>(builder);

                //var data = await dapper.QueryListAsync<Role>("t1.*", "usercenter_role t1",
                //    where: new DapperSql("EXISTS (SELECT * FROM usercenter_user_role t2 WHERE t2.RoleId=t1.Id AND t2.UserId=@UserId)", new { UserId = userId }),
                //    order: null);
                return Mapper.Map<List<RoleDto>>(data.ToList());
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        public async Task<UserDto> LoginAsync(string account, string password)
        {
            if (account.IsNullOrEmpty())
                throw new KuArgNullException("账户名不能为空！");

            if (password.IsNullOrEmpty())
                throw new KuArgNullException("密码不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                var entity = await _dapper.QueryOneAsync<User>(new { Account = account });
                if (entity == null || entity.IsDeleted)
                {
                    throw new KuException("账户不存在！");
                }
                if (!entity.CheckPassword(password))
                {
                    throw new KuException("账户或密码出错！");
                }
                if (!entity.IsEnable)
                {
                    throw new KuException("该账号已被禁止登陆！");
                }

                var dto = Mapper.Map<UserDto>(entity);
                dto.Password = "";

                //发送消息
                //await _eventPublisher.PublishAsync(new UserLoginEvent { UserId = entity.Id });

                return dto;
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        public async Task<UserDto> MobileLoginAsync(string mobile, string password)
        {
            if (mobile.IsNullOrEmpty())
                throw new KuArgNullException("手机号不能为空！");

            if (password.IsNullOrEmpty())
                throw new KuArgNullException("密码不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                var entity = await _dapper.QueryOneAsync<User>(new { Mobile = mobile });
                if (entity == null || entity.IsDeleted)
                {
                    throw new KuException("账户不存在！");
                }
                if (!entity.CheckPassword(password))
                {
                    throw new KuException("手机号或密码出错！");
                }
                if (!entity.IsEnable)
                {
                    throw new KuException("该账号已被禁止登陆！");
                }

                var dto = Mapper.Map<UserDto>(entity);
                dto.Password = "";

                //发送消息
                //await _eventPublisher.PublishAsync(new UserLoginEvent { UserId = entity.Id });

                return dto;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public async Task ChangePasswordAsync(long userId, string currentPwd, string newPwd)
        {
            if (currentPwd.IsNullOrEmpty())
            {
                //当前密码不能为空
                throw new KuArgNullException("当前密码不能为空！");
            }
            if (newPwd.IsNullOrEmpty())
            {
                //新密码不能为空
                throw new KuArgNullException("新密码不能为空！");
            }
            using (var _dapper = DapperFactory.Create())
            {
                var item = await _dapper.QueryOneAsync<User>(new { Id = userId });
                if (item == null)
                {
                    throw new KuDataNotFoundException("无法取得用户数据！");
                }

                if (!item.CheckPassword(currentPwd))
                {
                    throw new KuArgNullException("当前密码出错！");
                }

                item.Password = newPwd;
                item.EncryptPassword();

                await _dapper.UpdateAsync<User>(new { item.Password }, new { Id = userId });
            }
        }

        /// <summary>
        /// 用户资料保存
        /// </summary>
        public async Task SaveProfileAsync(UserDto dto)
        {
            User model = Mapper.Map<User>(dto);
            using (var _dapper = DapperFactory.Create())
            {
                var item = new {
                    model.NickName,
                    model.RealName,
                    model.Mobile,
                    model.HeadImage,
                    model.Email,
                    model.Sex,
                    model.Remarks,
                    model.QQ,
                    model.Signature,
                    model.WebsiteUrl
                };
                await _dapper.UpdateAsync<User>(item, new { model.Id });
            }
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        public async Task<bool> PasswordCheckAsync(long id, string password)
        {
            if (password.IsNullOrEmpty())
                throw new KuArgNullException("密码不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                var entity = await _dapper.QueryOneAsync<User>(new { Id = id });
                if (entity == null || entity.IsDeleted)
                {
                    throw new KuException("账户不存在！");
                }
                if (!entity.IsEnable)
                {
                    throw new KuException("该账号已被禁止登陆！");
                }
                return entity.CheckPassword(password);
            }
        }

        /// <summary>
        /// 登陆(第三方登录)
        /// </summary>
        public async Task<UserDto> OAuthLoginAsync(EmUserLoginType type, string openId)
        {
            if (openId.IsNullOrEmpty())
                throw new KuArgNullException("OpenId不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                User user = null;
                switch (type)
                {
                    case EmUserLoginType.QQ:
                        user = await _dapper.QueryOneAsync<User>(new { QQOpenId = openId, IsDeleted = false });
                        break;
                    case EmUserLoginType.Weixin:
                        break;
                    default:
                        break;
                }

                if (user == null)
                {
                    return null;
                }
                if (!user.IsEnable)
                {
                    throw new KuException("该账号已被禁止登陆！");
                }

                var dto = Mapper.Map<UserDto>(user);
                dto.Password = "";

                //发送消息
                //await _eventPublisher.PublishAsync(new UserLoginEvent { UserId = entity.Id });

                return dto;
            }
        }

        /// <summary>
        /// 绑定(第三方登录)
        /// </summary>
        public async Task OAuthBindAsync(long userId, EmUserLoginType type, string openId)
        {
            if (openId.IsNullOrEmpty())
                throw new KuArgNullException("OpenId不能为空！");

            using (var _dapper = DapperFactory.Create())
            {
                switch (type)
                {
                    case EmUserLoginType.QQ:
                        await _dapper.UpdateAsync<User>(new { QQOpenId = openId }, new { Id = userId });
                        break;
                    case EmUserLoginType.Weixin:
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        public async Task<UserDto> RegisterAsync(string mobile, string password)
        {
            if (mobile.IsNullOrEmpty())
                throw new KuArgNullException("手机号不能为空！");

            if (password.IsNullOrEmpty())
                throw new KuArgNullException("密码不能为空！");

            //新增
            using (var dapper = DapperFactory.Create())
            {
                var model = new User {
                    Account = mobile,
                    Password = password,
                    Mobile = mobile,
                    Sex = Domain.Enum.EmSex.Secret,
                    NickName = "手机用户" + mobile.Substring(7)
                };

                //格式
                if (!model.Mobile.IsMobile())
                {
                    throw new KuException("手机号格式不正确！");
                }
                //是否重复
                var count = await dapper.QueryCountAsync<User>(new { Mobile = model.Mobile });
                if (count > 0)
                {
                    throw new KuException("该手机号已被注册！");
                }

                //检查账户是否重复
                count = await dapper.QueryCountAsync<User>(new { Account = model.Account });
                if (count > 0)
                {
                    throw new KuException("该手机号已被注册！");
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                model.Factor = new Random().Next(9999);
                //密码加密处理
                model.EncryptPassword();

                using (var trans = dapper.BeginTrans())
                {
                    await dapper.InsertAsync(model);
                    trans.Commit();
                }

                var dto = Mapper.Map<UserDto>(model);
                dto.Password = "";
                return dto;
            }
        }

        /// <summary>
        /// 手机绑定
        /// </summary>
        public async Task MobileBindAsync(long userId, string mobile)
        {
            using (var _dapper = DapperFactory.Create())
            {
                var entity = await _dapper.QueryOneAsync<User>(new { Id = userId });
                if (entity.Account.Eq(entity.Mobile))
                {
                    var item = new
                    {
                        Account = mobile,
                        Mobile = mobile
                    };
                    await _dapper.UpdateAsync<User>(item, new { entity.Id });
                }
                else
                {
                    var item = new
                    {
                        Mobile = mobile
                    };
                    await _dapper.UpdateAsync<User>(item, new { entity.Id });
                }
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        public async Task ResetPasswordAsync(string mobile, string password)
        {
            if (password.IsNullOrEmpty())
            {
                //当前密码不能为空
                throw new KuArgNullException("密码不能为空！");
            }
            using (var _dapper = DapperFactory.Create())
            {
                var item = await _dapper.QueryOneAsync<User>(new { Mobile = mobile });
                if (item == null)
                {
                    throw new KuDataNotFoundException("无法取得用户数据！");
                }

                item.Password = password;
                item.EncryptPassword();

                await _dapper.UpdateAsync<User>(new { item.Password }, new { Id = item.Id });
            }
        }

        /// <summary>
        /// 用户头像保存
        /// </summary>
        public async Task SaveHeadImageAsync(long userId, string HeadImagePath)
        {
            using (var _dapper = DapperFactory.Create())
            {
                var item = new
                {
                    HeadImage = HeadImagePath
                };
                await _dapper.UpdateAsync<User>(item, new { Id = userId });
            }
        }
    }
}
