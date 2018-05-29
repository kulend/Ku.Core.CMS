using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Picture
{
    [Auth("materialcenter.picture")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SelectorModel : BasePage
    {
        private readonly IPictureService _service;

        public SelectorModel(IPictureService service)
        {
            _service = service;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(int page, int rows)
        {
            var data = await _service.GetListAsync(page, rows, null, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}