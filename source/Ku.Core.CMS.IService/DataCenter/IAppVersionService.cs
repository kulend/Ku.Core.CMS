//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：IAppVersionService.cs
// 功能描述：应用版本 业务逻辑接口类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-16 10:59
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;

namespace Ku.Core.CMS.IService.DataCenter
{
    public partial interface IAppVersionService
    {
        #region 自动创建的接口

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<AppVersionDto></returns>
        Task<List<AppVersionDto>> GetListAsync(AppVersionSearch where, string sort);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        Task<(int count, List<AppVersionDto> items)> GetListAsync(int page, int size, AppVersionSearch where, string sort);

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<AppVersionDto> GetByIdAsync(long id);

        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AppVersionDto dto);

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
