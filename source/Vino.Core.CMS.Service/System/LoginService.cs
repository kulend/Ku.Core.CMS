using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Extensions;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class LoginService: ILoginService
    {
        private VinoDbContext _context;
        private IUserRepository repository;
        public LoginService(VinoDbContext context, IUserRepository _repository)
        {
            this._context = context;
            this.repository = _repository;
        }

        /// <summary>
        /// 登陆处理
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User DoLogin(string account, string password)
        {
            if (string.IsNullOrEmpty(account))
                throw new VinoArgNullException("账户名不能为空！");

            if (string.IsNullOrEmpty(password))
                throw new VinoArgNullException("密码不能为空！");

            var entity = _context.Users.SingleOrDefault(x => x.Account.Equals(account, 
                                                StringComparison.OrdinalIgnoreCase));
            if (entity == null)
            {
                throw new VinoException("账户不存在!");
            }

            if (!entity.Password.Equals(password))
            {
                throw new VinoException("账户或密码出错!");
            }

            return entity;
        }



        public User Create(string account, string password)
        {
            if (string.IsNullOrEmpty(account))
                throw new VinoArgNullException("账户名不能为空！");

            if (string.IsNullOrEmpty(password))
                throw new VinoArgNullException("密码不能为空！");

            var entity = _context.Users.SingleOrDefault(x => x.Account.Equals(account,
                StringComparison.OrdinalIgnoreCase));

            if (entity != null)
            {
                throw new VinoException("账号已存在！");
            }

            entity = new User();
            entity.Id = DateTime.Now.Ticks;
            entity.Account = account;
            entity.Password = password;
            entity.IsEnable = true;
            entity.Name = account;
            entity.IsDeleted = false;
            entity.CreateTime = DateTime.Now;
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
