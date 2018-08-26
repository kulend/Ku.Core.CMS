using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Picture
{
    [Auth("materialcenter.picture")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SelectorModel : BasePage
    {
        private readonly IPictureService _service;
        private readonly IUserMaterialGroupService _groupService;

        public SelectorModel(IPictureService service, IUserMaterialGroupService groupService)
        {
            _service = service;
            _groupService = groupService;
        }

        public List<UserMaterialGroupDto> Groups { set; get; }

        public async Task OnGetAsync()
        {
            //取得用户素材分组
            Groups = await _groupService.GetListAsync(new UserMaterialGroupSearch { UserId = User.GetUserIdOrZero(), Type = Domain.Enum.MaterialCenter.EmUserMaterialGroupType.Picture }, null);
        }

        public async Task<IActionResult> OnPostAsync(int page, int rows, PictureSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}