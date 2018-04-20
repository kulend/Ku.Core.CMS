//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MenuService.cs
// 功能描述：菜单 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.CMS.IService.System;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.System
{
    public partial class MenuService : BaseService, IMenuService
    {
        protected readonly IMenuRepository _repository;

        #region 构造函数

        public MenuService(IMenuRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<MenuDto></returns>
        public async Task<List<MenuDto>> GetListAsync(MenuSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<MenuDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MenuDto> items)> GetListAsync(int page, int size, MenuSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<MenuDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MenuDto> GetByIdAsync(long id)
        {
            return Mapper.Map<MenuDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MenuDto dto)
        {
            Menu model = Mapper.Map<Menu>(dto);
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
                menu.Tag = model.Tag;
                _repository.Update(menu);
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
            foreach (var item in id)
            {
                //取得菜单信息
                var menu = await _repository.GetByIdAsync(item);
                if (menu == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                await _repository.DeleteAsync(id);
                if (menu.ParentId.HasValue)
                {
                    await UpdateMenuHasSub(menu.ParentId.Value);
                }
            }
            await _repository.SaveAsync();
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
		
        public Task<List<MenuDto>> GetParentsAsync(long parentId)
        {
            List<MenuDto> Gets()
            {
                return GetParents(parentId);
            }
            return Task.FromResult(Gets());
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
            return Mapper.Map<List<MenuDto>>(await query.ToListAsync());
        }

        public async Task<List<MenuDto>> GetMenuTreeAsync()
        {
            var queryable = _repository.GetQueryable();
            queryable = queryable.Where(u => u.ParentId == null);
            queryable = queryable.Include(x=>x.SubMenus).ThenInclude(x=>x.SubMenus);
            var query = queryable.OrderBy(x => x.OrderIndex);
            return Mapper.Map<List<MenuDto>>(await query.ToListAsync());
        }
		
        #endregion

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
            return Mapper.Map<List<MenuDto>>(list);
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
