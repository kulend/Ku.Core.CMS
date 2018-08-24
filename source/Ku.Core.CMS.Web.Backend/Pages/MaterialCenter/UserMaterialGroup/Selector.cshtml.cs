using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.UserMaterialGroup
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SelectorModel : BasePage
    {
        private readonly IUserMaterialGroupService _service;

        public SelectorModel(IUserMaterialGroupService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        //[Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(int page, int rows, UserMaterialGroupSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}