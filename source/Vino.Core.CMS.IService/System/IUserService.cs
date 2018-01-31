using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.IService.System
{
    public partial interface IUserService
    {
        #region 自动创建的接口

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<UserDto></returns>
        Task<List<UserDto>> GetListAsync(UserSearch where, string sort);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        Task<(int count, List<UserDto> items)> GetListAsync(int page, int size, UserSearch where, string sort);

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<UserDto> GetByIdAsync(long id);

        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserDto dto, long[] UserRoleIds);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task DeleteAsync(long id);

        #endregion

        #region 其他接口

        /// <summary>
        /// 取得用户角色列表
        /// </summary>
        Task<List<RoleDto>> GetUserRolesAsync(long userId);

        /// <summary>
        /// 登陆
        /// </summary>
        Task<UserDto> LoginAsync(string account, string password);

        /// <summary>
        /// 修改密码
        /// </summary>
        Task ChangePassword(long userId, string currentPwd, string newPwd);

        #endregion
    }
}
