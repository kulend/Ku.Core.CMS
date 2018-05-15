//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IAppFeedbackService.cs
// 功能描述：应用反馈 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.DataCenter
{
    public partial interface IAppFeedbackService : IBaseService<AppFeedback, AppFeedbackDto, AppFeedbackSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AppFeedbackDto dto);

        /// <summary>
        /// 处理反馈
        /// </summary>
        Task ResolveAsync(AppFeedbackDto dto);
    }
}
