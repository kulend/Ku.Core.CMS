//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ISmsService.cs
// 功能描述：短信 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:50
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.Communication;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Communication
{
    public partial interface ISmsService : IBaseService<Sms, SmsDto, SmsSearch>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        Task AddAsync(SmsDto dto);
    }
}
