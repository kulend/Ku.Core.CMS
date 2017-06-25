using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Entity.System;
using Vino.Core.CMS.Service.System.Dto;

namespace Vino.Core.CMS.Service.System
{
    public class RoleService : IRoleService
    {
        private VinoDbContext _context;

        public RoleService(VinoDbContext context)
        {
            this._context = context;
        }

        public List<RoleDto> GetList(int pageIndex, int pageSize, out int count)
        {
            int startRow = (pageIndex - 1) * pageSize;
            //取得总条数
            count = _context.Roles.Count();
            var query = _context.Roles.Where(x=>!x.IsDeleted).OrderByDescending(x=>x.CreateTime).Skip(startRow).Take(pageSize);
            return Mapper.Map<List<RoleDto>>(query.ToList());
        }

        public RoleDto GetById(long id)
        {
            var menu = _context.Roles.SingleOrDefault(x => x.Id == id);
            return Mapper.Map<RoleDto>(menu);
        }

        public void Save(RoleDto dto)
        {
            Role model = Mapper.Map<Role>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.IsDeleted = false;
                model.CreateTime = DateTime.Now;
                _context.Roles.Add(model);
            }
            else
            {
                //更新
                var role = _context.Roles.SingleOrDefault(x => x.Id == model.Id);
                if (role == null)
                {
                    throw new VinoDataNotFoundException("无法取得角色数据!");
                }

                role.Name = model.Name;
                role.Remarks = model.Remarks;
                _context.Roles.Update(role);
            }
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var role = _context.Roles.SingleOrDefault(x => x.Id == id);
            if (role == null)
            {
                throw new VinoDataNotFoundException("无法取得角色数据!");
            }
            if (!role.IsDeleted)
            {
                role.IsDeleted = true;
                _context.Roles.Update(role);
                _context.SaveChanges();
            }
        }
    }
}
