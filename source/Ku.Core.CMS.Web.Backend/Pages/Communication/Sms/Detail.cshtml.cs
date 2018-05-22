using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.Communication;
using Ku.Core.CMS.IService.Communication;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Communication.Sms
{
    /// <summary>
    /// 短信队列 详情页面
    /// </summary>
    [Auth("communication.sms.queue")]
    public class DetailModel : BasePage
    {
        private readonly ISmsQueueService _service;
        private readonly ISmsService _smsService;

        public DetailModel(ISmsQueueService service, ISmsService smsService)
        {
            _service = service;
            _smsService = smsService;
        }

        public SmsQueueDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new VinoDataNotFoundException();
            }
            var sms = await _smsService.GetByIdAsync(Dto.SmsId);
            if (sms == null || Dto.IsDeleted)
            {
                throw new VinoDataNotFoundException();
            }
            Dto.Sms = sms;
        }
    }
}
