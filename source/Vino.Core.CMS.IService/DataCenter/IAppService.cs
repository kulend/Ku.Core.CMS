//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：IAppService.cs
// 功能描述：应用 业务逻辑接口类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.DataCenter;
using Vino.Core.CMS.Domain.Entity.DataCenter;

namespace Vino.Core.CMS.IService.DataCenter
{
    public partial interface IAppService
    {
        #region 自动创建的接口

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<AppDto></returns>
        Task<List<AppDto>> GetListAsync(AppSearch where, string sort);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        Task<(int count, List<AppDto> items)> GetListAsync(int page, int size, AppSearch where, string sort);

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<AppDto> GetByIdAsync(long id);

        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AppDto dto);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteAsync(params long[] id);

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task RestoreAsync(params long[] id);

        #endregion

        #region 其他接口

        #endregion
    }
}
