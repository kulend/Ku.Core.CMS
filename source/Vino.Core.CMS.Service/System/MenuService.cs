using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class MenuService: IMenuService
    {
        private VinoDbContext _context;

        public MenuService(VinoDbContext context)
        {
            this._context = context;
        }

        public MenuDto GetById(long id)
        {
            var menu = _context.Menus.SingleOrDefault(x => x.Id == id);
            return Mapper.Map<MenuDto>(menu);
        }

        public List<MenuDto> GetMenusByParentId(long parentId)
        {
            List<Menu> menus = _context.Menus.Where(x => x.ParentId == parentId).OrderBy(x => x.OrderIndex).ToList();
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public void SaveMenu(MenuDto dto)
        {
            Menu model = Mapper.Map<Menu>(dto);
            if (model.Id == 0)
            {
                //新增
                string pCode = "";
                //取得父菜单               
                if (model.ParentId != 0)
                {
                    var pModel = _context.Menus.SingleOrDefault(x => x.Id == model.ParentId);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父菜单数据!");
                    }
                    pCode = pModel.Code;
                    if (!pModel.HasSubMenu)
                    {
                        pModel.HasSubMenu = true;
                        _context.Menus.Update(pModel);
                    }
                }
                //取得code
                string newCode = "";
                var maxCode = _context.Menus.Where(x => x.ParentId == model.ParentId).Max(x => x.Code);
                if (!string.IsNullOrEmpty(maxCode))
                {
                    int.TryParse(maxCode, out int code);
                    code++;
                    newCode = code.ToString().PadLeft(maxCode.Length, '0');
                }
                else
                {
                    newCode = pCode + "001";
                }
                model.Code = newCode;
                model.Id = ID.NewID();
                model.Type = 0;
                model.HasSubMenu = false;
                model.IsDeleted = false;
                model.CreateTime= DateTime.Now;
                _context.Menus.Add(model);
            }
            else
            {
                //更新
                var menu = _context.Menus.SingleOrDefault(x => x.Id == model.Id);
                if (menu == null)
                {
                    throw new VinoDataNotFoundException("无法取得菜单数据!");
                }

                menu.Type = model.Type;
                menu.Name = model.Name;
                menu.Url = model.Url;
                menu.Icon = model.Icon;
                menu.IsShow = model.IsShow;
                menu.OrderIndex = model.OrderIndex;
                menu.Remarks = model.Remarks;
                _context.Menus.Update(menu);
            }
            _context.SaveChanges();
        }

    }
}
