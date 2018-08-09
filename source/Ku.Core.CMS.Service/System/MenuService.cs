//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MenuService.cs
// 功能描述：菜单 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class MenuService : BaseService<Menu, MenuDto, MenuSearch>, IMenuService
    {
        #region 构造函数

        public MenuService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MenuDto dto)
        {
            Menu model = Mapper.Map<Menu>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    //取得父功能              
                    if (model.ParentId.HasValue)
                    {
                        var pModel = await dapper.QueryOneAsync<Menu>(new { Id = model.ParentId.Value });
                        if (pModel == null)
                        {
                            throw new KuDataNotFoundException("无法取得父级菜单数据!");
                        }
                        if (!pModel.HasSubMenu)
                        {
                            await dapper.UpdateAsync<Menu>(new { HasSubMenu = true }, new { pModel.Id });
                        }
                    }
                    else
                    {
                        model.ParentId = null;
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    await dapper.InsertAsync(model);

                    dapper.Commit();
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new
                    {
                        //这里进行赋值
                        model.Name,
                        model.AuthCode,
                        model.IsShow,
                        model.OrderIndex,
                        model.Icon,
                        model.Url,
                        model.Tag
                    };
                    await dapper.UpdateAsync<Menu>(item, new { model.Id });
                }
            }
        }

        #region 其他方法
		
        public async Task<List<MenuDto>> GetParentsAsync(long parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = new List<Menu>();
                async Task GetWhithParentAsync(long pid)
                {
                    var model = await dapper.QueryOneAsync<Menu>(new { Id = pid });
                    if (model != null)
                    {
                        if (model.ParentId.HasValue)
                        {
                            await GetWhithParentAsync(model.ParentId.Value);
                        }
                        list.Add(model);
                    }
                }

                await GetWhithParentAsync(parentId);
                return Mapper.Map<List<MenuDto>>(list);
            }
        }
		
        public async Task<List<MenuDto>> GetSubsAsync(long? parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = await dapper.QueryListAsync<Menu>(new { ParentId = parentId }, new { OrderIndex = "asc", CreateTime = "asc" });
                return Mapper.Map<List<MenuDto>>(list);
            }
        }

        public async Task<List<MenuDto>> GetMenuTreeAsync()
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = await dapper.QueryListAsync<Menu>(where: null, order: null);
                foreach (var item in list)
                {
                    item.SubMenus = new List<Menu>();
                }
                foreach (var item in list)
                {
                    if (item.ParentId.HasValue)
                    {
                        var p = list.FirstOrDefault(x => x.Id == item.ParentId.Value);
                        if (p != null)
                        {
                            p.SubMenus.Add(item);
                        }
                    }
                }

                return Mapper.Map<List<MenuDto>>(list.Where(x => x.ParentId == null).OrderBy(x => x.OrderIndex));
            }
        }
		
        #endregion
    }
}
