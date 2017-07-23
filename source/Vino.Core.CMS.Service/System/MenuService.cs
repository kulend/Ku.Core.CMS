using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class MenuService: IMenuService
    {
        private IMenuRepository repository;

        public MenuService(IMenuRepository _repository)
        {
            this.repository = _repository;
        }

        public Task<(int count, List<MenuDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize)
        {
            (int, List<MenuDto>) Gets()
            {
                int startRow = (pageIndex - 1) * pageSize;
                var queryable = repository.GetQueryable();
                if (parentId.HasValue)
                {
                    queryable = queryable.Where(u => u.ParentId == parentId);
                }
                else
                {
                    queryable = queryable.Where(u => u.ParentId == null);
                }
                var count = queryable.Count();
                var query = queryable.OrderBy(x=>x.OrderIndex).ThenBy(x => x.CreateTime).Skip(startRow).Take(pageSize);
                return (count, Mapper.Map<List<MenuDto>>(query.ToList()));
            }
            return Task.FromResult(Gets());
        }

        public async Task SaveAsync(MenuDto dto)
        {
            Menu model = Mapper.Map<Menu>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得父级
                if (model.ParentId.HasValue)
                {
                    var pModel = await repository.GetByIdAsync(model.ParentId.Value);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父级菜单数据!");
                    }
                }
                else
                {
                    model.ParentId = null;
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                await repository.InsertAsync(model);
            }
            else
            {
                //更新
                var menu = await repository.GetByIdAsync(model.Id);
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
                repository.Update(menu);
            }
            await repository.SaveAsync();
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
            return Mapper.Map<MenuDto>(await this.repository.GetByIdAsync(id));
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }

        private List<MenuDto> GetParents(long parentId)
        {
            var list = new List<Menu>();
            void GetModel(long pid)
            {
                var model = repository.FirstOrDefault(x => x.Id == pid);
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
            return Mapper.Map<List<MenuDto>>(list);
        }

        public async Task<List<MenuDto>> GetSubsAsync(long? parentId)
        {
            var queryable = repository.GetQueryable();
            if (parentId.HasValue)
            {
                queryable = queryable.Where(u => u.ParentId == parentId);
            }
            else
            {
                queryable = queryable.Where(u => u.ParentId == null);
            }

            var query = queryable.OrderBy(x=>x.OrderIndex).ThenBy(x => x.CreateTime);
            return Mapper.Map<List<MenuDto>>(await query.ToListAsync());
        }
    }
}
