using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.Web.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.CMS.Service.Material;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.EventBus.CAP;
using Vino.Core.Infrastructure.IdGenerator;
using Vino.Core.CMS.IService.Material;

namespace Vino.Core.CMS.Web.Backend.Areas.Material.Views.Picture
{
    [Area("Material")]
    [Auth("material.picture")]
    public class PictureController : BackendController
    {
        private IHostingEnvironment _env;
        private IPictureService _service;
        public PictureController(IHostingEnvironment env,
            IPictureService service)
        {
            this._env = env;
            this._service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows)
        {
            var data = await _service.GetListAsync(page, rows, null, null);
            return PagerData(data.items, page, rows, data.count);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public IActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [Auth("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> file)
        {
            string yyyymm = DateTime.Now.ToyyyyMM();
            string oppositePath = $"/upload/pictures/{User.GetUserIdOrZero()}/{yyyymm}/";
            var folder = _env.WebRootPath + oppositePath;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            foreach (var item in file)
            {
                PictureDto model = new PictureDto();
                model.Id = ID.NewID();

                string suffix = item.FileName.Split('.').Last().ToLower();
                var saveName = model.Id + "_original." + suffix;

                using (var fileStream = new FileStream(Path.Combine(folder, saveName), FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }

                model.Title = item.FileName;
                model.FileName = saveName;
                model.Folder = oppositePath;
                model.FilePath = oppositePath + saveName;
                model.FileSize = item.Length;
                model.FileType = suffix;
                model.UploadUserId = User.GetUserIdOrZero();
                await this._service.AddAsync(model);
            }
            return Json(true);
        }

        [Auth("select")]
        public IActionResult Select()
        {
            return View();
        }

        [Auth("select")]
        public async Task<IActionResult> GetSelectList(int page, int rows)
        {
            var data = await _service.GetListAsync(page, rows, null, null);
            return PagerData(data.items, page, rows, data.count);
        }
    }
}
