using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Picture
{
    [Auth("materialcenter.picture")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class UploadModel : BasePage
    {
        private readonly IHostingEnvironment _env;
        private readonly IPictureService _service;
        private readonly IMaterialCenterConfigService _configService;

        public UploadModel(IHostingEnvironment env, IPictureService service, IMaterialCenterConfigService configService)
        {
            _env = env;
            _service = service;
            _configService = configService;
        }

        public void OnGet()
        {

        }

        [Auth("upload")]
        public async Task<IActionResult> OnPostAsync(ICollection<IFormFile> file)
        {
            string yyyymm = DateTime.Now.ToyyyyMM();
            var config = await _configService.GetAsync();
            string oppositePath = $"pictures/{User.GetUserIdOrZero()}/{yyyymm}/";
            var folder = _env.WebRootPath + "/upload/" + oppositePath;
            if (config != null && !string.IsNullOrEmpty(config.PictureSavePath))
            {
                folder = Path.Combine(config.PictureSavePath, oppositePath);
            }
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
                await _service.AddAsync(model);
            }
            return Json(true);
        }
    }
}