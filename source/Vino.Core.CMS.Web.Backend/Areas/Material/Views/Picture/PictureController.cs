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

namespace Vino.Core.CMS.Web.Backend.Areas.Material.Views.Picture
{
    [Area("Material")]
    [Auth("material.picture")]
    public class PictureController : BaseController
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

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [Auth("material.picture.upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile[] file)
        {
            string folder = _env.WebRootPath + $@"\upload\pictures\";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            foreach (var item in file)
            {
                md5.Clear();
                string suffix = item.FileName.Split('.')[1];
                var saveName = Guid.NewGuid().ToString("N") + "." + suffix;
                var filePath = folder + saveName;
                var md5Code = "";
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    await item.CopyToAsync(fs);
                    md5Code = CryptHelper.EncryptMD5(fs);
                    fs.Flush();
                }

                PictureDto model = new PictureDto();
                model.Title = item.FileName;
                model.FileName = item.FileName;
                model.FilePath = filePath;
                model.FileSize = item.Length;
                model.FileType = suffix.ToLower();
                model.Md5Code = md5Code;
                model.UploadUserId = User.GetUserIdOrZero();
                await this._service.SaveAsync(model);
            }
            return Json(true);
        }
    }
}
