//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IRoleFunctionService.cs
// 功能描述：角色功能关联 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-17 09:34
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IRoleFunctionService : IBaseService<RoleFunction, RoleFunctionDto, RoleFunctionSearch>
    {
    }
}
