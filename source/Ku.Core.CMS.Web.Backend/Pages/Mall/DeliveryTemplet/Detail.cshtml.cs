using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Mall.DeliveryTemplet
{
    /// <summary>
    /// 配送模板 详情页面
    /// </summary>
    [Auth("mall.deliverytemplet")]
    public class DetailModel : BasePage
    {
        private readonly IDeliveryTempletService _service;

        public DetailModel(IDeliveryTempletService service)
        {
            this._service = service;
        }

        public DeliveryTempletDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new KuDataNotFoundException();
            }
        }
    }
}
