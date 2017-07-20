using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class UserService : IUserService
    {
        private VinoDbContext _context;

        public UserService(VinoDbContext context)
        {
            this._context = context;
        }

        public List<UserDto> GetUserList(int pageIndex, int pageSize, out int count)
        {
            int startRow = (pageIndex - 1) * pageSize;
            //取得总条数
            count = _context.Users.Count();
            var query = _context.Users.OrderByDescending(x=>x.CreateTime).Skip(startRow).Take(pageSize);
            return Mapper.Map<List<UserDto>>(query.ToList());
        }

        public UserDto GetById(long id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            return Mapper.Map<UserDto>(user);
        }

        public void SaveUser(UserDto dto)
        {
            User model = Mapper.Map<User>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.IsDeleted = false;
                model.CreateTime = DateTime.Now;
                _context.Users.Add(model);
            }
            else
            {
                //更新
                var user = _context.Users.SingleOrDefault(x => x.Id == model.Id);
                if (user == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据!");
                }

                user.Account = model.Account;
                user.Name = model.Name;
                user.Mobile = model.Mobile;
                user.IsEnable = model.IsEnable;
                user.Password = model.Password;
                user.Remarks = model.Remarks;
                _context.Users.Update(user);
            }
            _context.SaveChanges();
        }
    }
}
