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

namespace Ku.Core.CMS.Web.Backend.Pages.Content.Article
{
    /// <summary>
    /// 文章 编辑页面
    /// </summary>
    [Auth("content.article")]
    public class EditModel : BasePage
    {
        private readonly IArticleService _service;
        private readonly IColumnService _service2;
        private readonly IAlbumService _service3;

        public EditModel(IArticleService service, IColumnService service2, IAlbumService service3)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
        }

        [BindProperty]
        public ArticleDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long? ColumnId, long? albumId)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new KuDataNotFoundException();
                }
                if (Dto.ColumnId.HasValue)
                {
                    Dto.Column = await _service2.GetByIdAsync(Dto.ColumnId.Value);
                    if (Dto.Column == null)
                    {
                        throw new KuDataNotFoundException("数据出错!");
                    }
                }
                if (Dto.AlbumId.HasValue)
                {
                    Dto.Album = await _service3.GetByIdAsync(Dto.AlbumId.Value);
                    if (Dto.Album == null)
                    {
                        throw new KuDataNotFoundException("数据出错!");
                    }
                }

                ViewData["Mode"] = "Edit";
            }
            else
            {
                Dto = new ArticleDto();
                if (albumId.HasValue)
                {
                    Dto.ContentType = Domain.Enum.Content.EmArticleContentType.Video;
                    Dto.AlbumId = albumId.Value;
                    Dto.Album = await _service3.GetByIdAsync(albumId.Value);
                    if (Dto.Album == null)
                    {
                        throw new KuDataNotFoundException("参数出错!");
                    }
                }
                else
                {
                    Dto.ContentType = Domain.Enum.Content.EmArticleContentType.RichText;
                }
                Dto.IsPublished = true;
                if (ColumnId.HasValue)
                {
                    Dto.ColumnId = ColumnId.Value;
                    Dto.Column = await _service2.GetByIdAsync(ColumnId.Value);
                    if (Dto.Column == null)
                    {
                        throw new KuDataNotFoundException("参数出错!");
                    }
                }
                ViewData["Mode"] = "Add";
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

            await _service.SaveAsync(Dto);
            return Json(true);
        }
    }
}
