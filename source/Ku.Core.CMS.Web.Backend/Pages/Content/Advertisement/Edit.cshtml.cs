using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Advertisement
{
    /// <summary>
    /// 广告 编辑页面
    /// </summary>
    [Auth("content.advertisement")]
    public class EditModel : BasePage
    {
        private readonly IAdvertisementService _service;
        private readonly IAdvertisementBoardService _boardService;

        public EditModel(IAdvertisementService service, IAdvertisementBoardService boardService)
        {
            _service = service;
            _boardService = boardService;
        }

        [BindProperty]
        public AdvertisementDto Dto { set; get; }

        /// <summary>
        /// 广告位列表
        /// </summary>
        public IEnumerable<AdvertisementBoardDto> Boards { get; set; }

        [BindProperty]
        public long[] BindBoards { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException();
                }
                BindBoards = (await _service.GetAdvertisementBoardsAsync(Dto.Id)).Select(x => x.Id).ToArray();
                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new AdvertisementDto();
                ViewData["Mode"] = "Add";
            }

            //广告位列表取得
            Boards = await _boardService.GetListAsync(new AdvertisementBoardSearch { IsDeleted = false}, null);

            if (BindBoards == null)
            {
                BindBoards = new long[] { };
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new KuArgNullException();
            }

            await _service.SaveAsync(Dto, BindBoards);
            return Json(true);
        }
    }
}
