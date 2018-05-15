//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserActionLogService.cs
// 功能描述：用户操作记录 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;

namespace Ku.Core.CMS.Service.System
{
    public partial class UserActionLogService : BaseService<UserActionLog, UserActionLogDto, UserActionLogSearch>, IUserActionLogService
    {
        #region 构造函数

        public UserActionLogService()
        {
        }

        #endregion
    }
}
