using Dnc.Extensions.Dapper;
using Dnc.Extensions.Dapper.Builders;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Tools.ImageCompressor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Job
{
    [DisallowConcurrentExecution]
    public class PictureMaterialJob : BaseJob
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public override async Task Run(IJobExecutionContext context)
        {
            logger.Info("开始处理图片素材事务...");
            await Console.Out.WriteLineAsync("开始处理图片素材事务...");

            var count = 0;

            IImageCompressor imageCompressor = _provider.GetService<IImageCompressor>();
            IMaterialCenterConfigService _configService = _provider.GetService<IMaterialCenterConfigService>();
            MaterialCenterConfig config = await _configService.GetAsync();
            //var folder = Path.Combine(config.PictureSavePath, oppositePath);
            //if (config != null && !string.IsNullOrEmpty(config.PictureSavePath))
            //{
            //    folder = Path.Combine(config.PictureSavePath, oppositePath);
            //}

            //取得待压缩列表
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<Picture>().From<Picture>()
                    .Where(new ConditionBuilder().Equal<Picture>(m => m.IsCompressed, false)
                            .And().Equal<Picture>(m => m.IsDeleted, false));

                var items = await dapper.QueryListAsync<Picture>(builder);

                foreach (var item in items)
                {
                    //判断文件是否存在，如果不存在则自动删除该条数据
                    if (!File.Exists(Path.Combine(config.PictureSavePath, item.FilePath)))
                    {
                        await dapper.DeleteAsync<Picture>(new { item.Id });
                        continue;
                    }
                    if ("png".EqualOrdinalIgnoreCase(item.FileType)
                        || "jpg".EqualOrdinalIgnoreCase(item.FileType)
                        || "jpeg".EqualOrdinalIgnoreCase(item.FileType))
                    {
                        var fileName = item.Id + "." + item.FileType;
                        var filePath = item.Folder + fileName;
                        try
                        {
                            //压缩图片
                            await imageCompressor.Compress(Path.Combine(config.PictureSavePath, item.FilePath),
                                    Path.Combine(config.PictureSavePath, filePath));

                            //缩略图
                            var thumbPath = item.Folder + item.Id + "_thumb." + item.FileType;
                            await imageCompressor.Resize(Path.Combine(config.PictureSavePath, item.FilePath),
                                    Path.Combine(config.PictureSavePath, thumbPath), 
                                    new { method = "fit", width = 320, height = 320 });

                            var fileInfo = new FileInfo(Path.Combine(config.PictureSavePath, item.FilePath));

                            var model = new {
                                FilePath = filePath,
                                FileName = fileName,
                                ThumbPath = thumbPath,
                                IsCompressed = true,
                                FileSize = fileInfo.Length
                            };
                            await dapper.UpdateAsync<Picture>(model, new { item.Id });
                        }
                        catch(Exception ex)
                        {
                            logger.Error(ex);
                            await Console.Out.WriteLineAsync(ex.ToString());
                        }
                    }
                    else
                    {
                        await dapper.UpdateAsync<Picture>(new { IsCompressed = true, ThumbPath = item.FilePath }, new { item.Id });
                    }
                    count++;
                }
            }

            logger.Info($"共处理{count}张照片");
            await Console.Out.WriteLineAsync($"共处理{count}张照片");

            logger.Info($"结束处理图片素材事务...");
            await Console.Out.WriteLineAsync("结束处理图片素材事务...");
        }

    }
}
