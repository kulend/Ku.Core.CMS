//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：RoleFunctionService.cs
// 功能描述：角色功能关联 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-17 09:34
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.UserCenter
{
    public partial class RoleFunctionService : BaseService<RoleFunction, RoleFunctionDto, RoleFunctionSearch>, IRoleFunctionService
    {
        #region 构造函数

        public RoleFunctionService()
        {
        }

        #endregion
    }
}
