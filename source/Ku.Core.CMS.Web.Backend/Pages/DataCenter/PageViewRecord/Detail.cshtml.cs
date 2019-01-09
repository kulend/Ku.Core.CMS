using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.PageViewRecord
{
    /// <summary>
    /// 页面浏览记录 详情页面
    /// </summary>
    [Auth("datacenter.pageviewrecord")]
    public class DetailModel : BasePage
    {
        private readonly IPageViewRecordService _service;

        public DetailModel(IPageViewRecordService service)
        {
            _service = service;
        }

        [BindProperty]
        public PageViewRecordDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("detail")]
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
