using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Picture
{
    /// <summary>
    /// 图片素材 详情页面
    /// </summary>
    [Auth("materialcenter.picture")]
    public class DetailModel : BasePage
    {
        private readonly IPictureService _service;

        public DetailModel(IPictureService service)
        {
            this._service = service;
        }

        public PictureDto Dto { set; get; }

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
