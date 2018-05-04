using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Domain.Entity.Membership;
using Vino.Core.CMS.IService.Membership;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Pages.Membership.MemberAddress
{
    /// <summary>
    /// 会员地址 详情页面
    /// </summary>
    [Auth("membership.memberaddress")]
    public class DetailModel : BasePage
    {
        private readonly IMemberAddressService _service;

        public DetailModel(IMemberAddressService service)
        {
            this._service = service;
        }

        public MemberAddressDto Dto { set; get; }

        [Auth("view")]
        public async Task OnGetAsync(long id)
        {
            Dto = await _service.GetByIdAsync(id);
            if (Dto == null)
            {
                throw new VinoDataNotFoundException();
            }
        }
    }
}
