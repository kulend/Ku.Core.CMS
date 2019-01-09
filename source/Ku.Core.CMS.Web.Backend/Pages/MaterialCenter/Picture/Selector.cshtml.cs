using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Extensions;
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
        private readonly IMaterialCenterConfigService _configService;

        public SelectorModel(IPictureService service, IUserMaterialGroupService groupService, IMaterialCenterConfigService configService)
        {
            _service = service;
            _groupService = groupService;
            _configService = configService;
        }

        public List<UserMaterialGroupDto> Groups { set; get; }

        public async Task OnGetAsync()
        {
            //取得用户素材分组
            Groups = await _groupService.GetListAsync(new UserMaterialGroupSearch { UserId = User.GetUserIdOrZero(), Type = Domain.Enum.MaterialCenter.EmUserMaterialGroupType.Picture }, null);
        }

        public async Task<IActionResult> OnPostAsync(int page, int rows, PictureSearch where)
        {
            var config = await _configService.GetAsync();

            var data = await _service.GetListAsync(page, rows, where, "CreateTime desc");
            if (config.PictureUrl.IsNotNullOrEmpty())
            {
                foreach (var item in data.items)
                {
                    item.Url = config.PictureUrl.Contact(item.FilePath);
                    if (!string.IsNullOrEmpty(item.ThumbPath))
                    {
                        item.ThumbUrl = config.PictureUrl.Contact(item.ThumbPath);
                    }
                }
            }
            return PagerData(data.items, page, rows, data.count);
        }
    }
}