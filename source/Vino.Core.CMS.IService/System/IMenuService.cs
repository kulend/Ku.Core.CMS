using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.IService.System
{
    public partial interface IMenuService
    {
        #region 自动创建的接口

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<MenuDto></returns>
        Task<List<MenuDto>> GetListAsync(MenuSearch where, string sort);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        Task<(int count, List<MenuDto> items)> GetListAsync(int page, int size, MenuSearch where, string sort);

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<MenuDto> GetByIdAsync(long id);

        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MenuDto dto);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        #endregion

        #region 其他接口

        Task<List<MenuDto>> GetParentsAsync(long parentId);

        Task<List<MenuDto>> GetSubsAsync(long? parentId);

        Task<List<MenuDto>> GetMenuTreeAsync();

        #endregion
    }
}
