using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
using Ku.Core.Infrastructure.IdGenerator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Video
{
    [Auth("materialcenter.video")]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class UploadModel : BasePage
    {
        private readonly IHostingEnvironment _env;
        private readonly IVideoService _service;
        private readonly IMaterialCenterConfigService _configService;
        private readonly IUserMaterialGroupService _groupService;
        private readonly ILogger<UploadModel> _logger;

        public UploadModel(IHostingEnvironment env, IVideoService service, 
            IMaterialCenterConfigService configService, IUserMaterialGroupService groupService,
            ILogger<UploadModel> logger)
        {
            _env = env;
            _service = service;
            _configService = configService;
            _groupService = groupService;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        [Auth("upload")]
        public async Task<IActionResult> OnPostAsync(ICollection<IFormFile> file)
        {
            string yyyymm = DateTime.Now.ToyyyyMM();
            var config = await _configService.GetAsync();
            string oppositePath = $"videos/{User.GetUserIdOrZero()}/{yyyymm}/";
            var folder = _env.WebRootPath + "/upload/" + oppositePath;
            if (config != null && !string.IsNullOrEmpty(config.PictureSavePath))
            {
                folder = Path.Combine(config.PictureSavePath, oppositePath);
            }
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            //long[] groupIds = groups.SplitToInt64();
            //检查素材分组与用户关系
            //foreach (var id in groupIds)
            //{
            //    var group = await _groupService.GetByIdAsync(id);
            //    if (group == null || group.Type != Domain.Enum.MaterialCenter.EmUserMaterialGroupType.Picture 
            //        || group.UserId != User.GetUserIdOrZero())
            //    {
            //        throw new KuException("选择的素材分组不正确！");
            //    }
            //}
            foreach (var item in file)
            {
                VideoDto model = new VideoDto();
                model.Id = ID.NewID();

                string suffix = item.FileName.Split('.').Last().ToLower();
                var saveName = model.Id + "." + suffix;

                var path = Path.Combine(folder, saveName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }

                //try
                //{
                //    model.Md5Code = VideoHelper.GetVideoDuration(path);
                //}
                //catch (Exception ex)
                //{
                //    _logger.LogError(ex.Message);
                //}

                //try
                //{
                //    model.ThumbPath = VideoHelper.GetPreviewImage(path);
                //}
                //catch (Exception ex)
                //{
                //    _logger.LogError(ex.Message);
                //}

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