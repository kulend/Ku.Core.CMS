using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class UserService : IUserService
    {
        private VinoDbContext context;
        private IUserRepository repository;
        private IRoleRepository roleRepository;
        public UserService(VinoDbContext _context, 
            IUserRepository _repository
            , IRoleRepository _roleRepository)
        {
            this.context = _context;
            this.repository = _repository;
            this.roleRepository = _roleRepository;
        }

        public async Task<(int count, List<UserDto> items)> GetListAsync(int page, int size)
        {
            var data = await repository.PageQueryAsync(page, size, null, "CreateTime asc");
            return (data.count, Mapper.Map<List<UserDto>>(data.items));
        }

        public async Task<UserDto> GetByIdAsync(long id)
        {
            return Mapper.Map<UserDto>(await this.repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(UserDto dto, long[] UserRoleIds)
        {
            User model = Mapper.Map<User>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查账户是否重复
                var cnt = await repository.GetQueryable().Where(x => x.Account.EqualOrdinalIgnoreCase(model.Account)).CountAsync();
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
                    cnt = await repository.GetQueryable().Where(x => x.Mobile.EqualOrdinalIgnoreCase(model.Mobile)).CountAsync();
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
                await repository.InsertAsync(model);

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
                var item = await repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据！");
                }

                item.Account = model.Account;
                item.Name = model.Name;
                item.Mobile = model.Mobile;
                item.IsEnable = model.IsEnable;
                if (!model.Password.EqualOrdinalIgnoreCase("the password has not changed"))
                {
                    item.Password = model.Password;
                    //密码设置
                    item.EncryptPassword();
                }
                
                item.Remarks = model.Remarks;
                repository.Update(item);

                //角色处理
                var currentRoles = await context.UserRoles.Where(x => x.UserId == item.Id).ToListAsync();
                foreach (var roleId in UserRoleIds.Where(x=>!currentRoles.Any(i=>i.RoleId == x)))
                {
                    UserRole ur = new UserRole();
                    ur.UserId = model.Id;
                    ur.RoleId = roleId;
                    await context.UserRoles.AddAsync(ur);
                }
                context.UserRoles.RemoveRange(currentRoles.Where(x => !UserRoleIds.Contains(x.RoleId)));
            }
            await repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }

        #region 用户角色

        /// <summary>
        /// 取得用户角色列表
        /// </summary>
        public async Task<List<RoleDto>> GetUserRolesAsync(long userId)
        {
            var queryable = context.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId);
            var items = Mapper.Map<List<RoleDto>>((await queryable.ToListAsync()).Select(x=>x.Role));
            return items;
        }

        #endregion

        #region 登陆

        public async Task<UserDto> LoginAsync(string account, string password)
        {
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
            return dto;
        }

        #endregion

    }
}
