//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IFunctionService.cs
// 功能描述：功能 业务逻辑接口类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface IFunctionService : IBaseService<Function, FunctionDto, FunctionSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(FunctionDto dto);

        #region 其他接口

        Task<List<FunctionDto>> GetParentsAsync(long parentId);

        Task<List<FunctionDto>> GetSubsAsync(long? parentId);

        Task<bool> CheckUserAuth(long userId, string authCode);

        Task<List<string>> GetUserAuthCodesAsync(long userId, bool encrypt = false);

        #endregion
    }
}
