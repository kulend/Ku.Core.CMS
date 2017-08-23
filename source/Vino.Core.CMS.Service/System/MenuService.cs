using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.System
{
    public partial class MenuService
    {
        public async Task<(int count, List<MenuDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize)
        {
            var data = await _repository.PageQueryAsync(pageIndex, pageSize, function => function.ParentId == parentId, "");

            return (data.count, _mapper.Map<List<MenuDto>>(data.items));
        }

        public async Task SaveAsync(MenuDto dto)
        {
            Menu model = _mapper.Map<Menu>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得父级
                if (model.ParentId.HasValue)
                {
                    var pModel = await _repository.GetByIdAsync(model.ParentId.Value);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父级菜单数据!");
                    }
                    if (!pModel.HasSubMenu)
                    {
                        pModel.HasSubMenu = true;
                        _repository.Update(pModel);
                    }
                }
                else
                {
                    model.ParentId = null;
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var menu = await _repository.GetByIdAsync(model.Id);
                if (menu == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }

                menu.Name = model.Name;
                menu.AuthCode = model.AuthCode;
                menu.IsShow = model.IsShow;
                menu.OrderIndex = model.OrderIndex;
                menu.Icon = model.Icon;
                menu.Url = model.Url;
                _repository.Update(menu);
            }
            await _repository.SaveAsync();
        }

        public Task<List<MenuDto>> GetParentsAsync(long parentId)
        {
            List<MenuDto> Gets()
            {
                return GetParents(parentId);
            }
            return Task.FromResult(Gets());
        }

        public async Task<MenuDto> GetByIdAsync(long id)
        {
            return _mapper.Map<MenuDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task DeleteAsync(long id)
        {
            //取得菜单信息
            var menu = await _repository.GetByIdAsync(id);
            if (menu == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }
            await _repository.DeleteAsync(id);
            if (menu.ParentId.HasValue)
            {
                await UpdateMenuHasSub(menu.ParentId.Value);
            }
            await _repository.SaveAsync();
        }

        public async Task<List<MenuDto>> GetSubsAsync(long? parentId)
        {
            var queryable = _repository.GetQueryable();
            if (parentId.HasValue)
            {
                queryable = queryable.Where(u => u.ParentId == parentId);
            }
            else
            {
                queryable = queryable.Where(u => u.ParentId == null);
            }

            var query = queryable.OrderBy(x=>x.OrderIndex).ThenBy(x => x.CreateTime);
            return _mapper.Map<List<MenuDto>>(await query.ToListAsync());
        }

        public async Task<List<MenuDto>> GetMenuTreeAsync()
        {
            var queryable = _repository.GetQueryable();
            queryable = queryable.Where(u => u.ParentId == null);
            queryable = queryable.Include(x=>x.SubMenus);
            var query = queryable.OrderBy(x => x.OrderIndex);
            return _mapper.Map<List<MenuDto>>(await query.ToListAsync());
        }

        #region private

        private List<MenuDto> GetParents(long parentId)
        {
            var list = new List<Menu>();
            void GetModel(long pid)
            {
                var model = _repository.FirstOrDefault(x => x.Id == pid);
                if (model != null)
                {
                    if (model.ParentId.HasValue)
                    {
                        GetModel(model.ParentId.Value);
                    }
                    list.Add(model);
                }
            }
            GetModel(parentId);
            return _mapper.Map<List<MenuDto>>(list);
        }

        private async Task UpdateMenuHasSub(long id)
        {
            var menu = await _repository.GetByIdAsync(id);
            if (menu != null)
            {
                var cnt = await _repository.GetQueryable().Where(u => u.ParentId == menu.Id).CountAsync();
                if (cnt == 0 && menu.HasSubMenu)
                {
                    menu.HasSubMenu = false;
                    _repository.Update(menu);
                }
                else if (cnt > 0 && !menu.HasSubMenu)
                {
                    menu.HasSubMenu = true;
                    _repository.Update(menu);
                }
            }
        }

        #endregion
    }
}
