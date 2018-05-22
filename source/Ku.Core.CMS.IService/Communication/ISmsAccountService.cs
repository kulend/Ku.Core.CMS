//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ISmsAccountService.cs
// 功能描述：短信账户 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:11
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.Communication;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Communication
{
    public partial interface ISmsAccountService : IBaseService<SmsAccount, SmsAccountDto, SmsAccountSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(SmsAccountDto dto);
    }
}
