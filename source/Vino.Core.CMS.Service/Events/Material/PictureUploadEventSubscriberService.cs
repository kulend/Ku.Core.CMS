using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Material;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.EventBus;
using Vino.Core.EventBus.CAP;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Tools.ImageCompressor;

namespace Vino.Core.CMS.Service.Events.Material
{
    public class PictureUploadEventSubscriberService: IEventSubscriberService
    {
        protected readonly IPictureRepository _repository;
        private IHostingEnvironment _env;
        private IImageCompressor _imageCompressor;
        private readonly IEventPublisher _eventPublisher;

        public PictureUploadEventSubscriberService(IPictureRepository repository, 
            IHostingEnvironment env,
            IImageCompressor imageCompressor,
            IEventPublisher eventPublisher)
        {
            this._repository = repository;
            this._env = env;
            this._imageCompressor = imageCompressor;
            this._eventPublisher = eventPublisher;
        }

        /// <summary>
        /// 图片压缩处理
        /// </summary>
        [EventSubscribe("material_picture_compress")]
        public async Task Compress(PictureDto dto)
        {
            var model = await _repository.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return;
            }
            if (model.IsCompressed)
            {
                return;
            }
            if ("png".EqualOrdinalIgnoreCase(model.FileType) 
                || "jpg".EqualOrdinalIgnoreCase(model.FileType)
                || "jpeg".EqualOrdinalIgnoreCase(model.FileType))
            {
                var fileName = model.Id + "." + model.FileType;
                var filePath = model.Folder + fileName;
                try
                {
                    //压缩图片
                    await _imageCompressor.Compress(_env.WebRootPath + model.FilePath,
                            _env.WebRootPath + filePath);

                    //缩略图
                    var thumbPath = model.Folder + model.Id + "_thumb." + model.FileType;
                    await _imageCompressor.Resize(_env.WebRootPath + filePath,
                            _env.WebRootPath + thumbPath, new { method = "fit", width = 240, height = 240 });

                    model.FilePath = filePath;
                    model.FileName = fileName;
                    model.ThumbPath = thumbPath;
                    var fileInfo = new FileInfo(_env.WebRootPath + filePath);
                    model.FileSize = fileInfo.Length;
                    model.IsCompressed = true;
                    using (var trans = await _repository.BeginTransactionAsync())
                    {
                        _repository.Update(model);
                        await _repository.SaveAsync();
                        trans.Commit();
                    }
                }
                catch
                {
                    //尝试压缩3次
                    if (dto.TryCompressCount < 3)
                    {
                        dto.TryCompressCount = dto.TryCompressCount + 1;
                        //发送消息
                        await _eventPublisher.PublishAsync("material_picture_compress", dto);
                    }
                }
            }
        }

        /// <summary>
        /// 文件计算MD5
        /// </summary>
        [EventSubscribe("material_picture_upload")]
        public async Task Md5(PictureDto dto)
        {
            var model = await _repository.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return;
            }
            var filePath = _env.WebRootPath + model.FilePath;
            try
            {
                string md5Code = string.Empty;
                FileStream file = new FileStream(filePath, FileMode.Open);
                using (MD5 md5 = new MD5CryptoServiceProvider())
                {
                    byte[] retVal = md5.ComputeHash(file);
                    file.Close();
                    md5Code = retVal.Format("x2");
                }

                //更新
                model.Md5Code = md5Code;
                _repository.Update(model);
                await _repository.SaveAsync();

                //发送消息
                await _eventPublisher.PublishAsync("material_picture_compress", new PictureDto { Id = model.Id });
            }
            catch (Exception)
            {
                
            }
        }
    }
}
