using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.Domain.Enum.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
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

        public EmUserMaterialGroupType Type { set; get; }

        public void OnGet(EmUserMaterialGroupType type = EmUserMaterialGroupType.Picture)
        {
            Type = type;
        }

        /// <summary>
        /// 取得列表数据
        /// </summary>
        //[Auth("view")]
        public async Task<IActionResult> OnPostDataAsync(UserMaterialGroupSearch where)
        {
            if (where == null)
            {
                where = new UserMaterialGroupSearch();
            }
            where.UserId = User.GetUserIdOrZero();
            var data = await _service.GetListAsync(1, 999, where, null);
            return PagerData(data.items, 1, 999, data.count);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [Auth("delete")]
        public async Task<IActionResult> OnPostDeleteAsync(params long[] id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }
    }
}