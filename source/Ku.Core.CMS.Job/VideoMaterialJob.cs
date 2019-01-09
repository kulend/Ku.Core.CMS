using Dnc.Extensions.Dapper;
using Dnc.Extensions.Dapper.Builders;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
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
    public class VideoMaterialJob : BaseJob
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public override async Task Run(IJobExecutionContext context)
        {
            logger.Info("开始处理视频素材事务...");
            await Console.Out.WriteLineAsync("开始处理视频素材事务...");

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
                var builder = new QueryBuilder().Select<Video>().From<Video>()
                    .Where(new ConditionBuilder().IsNullOrEmpty<Video>(x => x.ThumbPath)
                            .And().Equal<Video>(m => m.IsDeleted, false));

                var items = await dapper.QueryListAsync<Video>(builder);

                foreach (var item in items)
                {
                    //判断文件是否存在
                    if (!File.Exists(Path.Combine(config.PictureSavePath, item.FilePath)))
                    {
                        continue;
                    }

                    try
                    {
                        //缩略图
                        var thumbPath = item.Folder + item.Id + "_thumb.jpg";
                        var thumb = VideoHelper.GetPreviewImage(Path.Combine(config.PictureSavePath, item.FilePath), 
                            Path.Combine(config.PictureSavePath, thumbPath), 320, 180, 1);
                        if (thumb != null)
                        {
                            thumb = thumbPath;
                        }
                        //时长
                        var duration = VideoHelper.GetVideoDuration(Path.Combine(config.PictureSavePath, item.FilePath));

                        await dapper.UpdateAsync<Video>(new { ThumbPath = thumb , Duration = duration }, new { item.Id });
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                        await Console.Out.WriteLineAsync(ex.ToString());
                    }
                    count++;
                }
            }

            logger.Info($"共处理{count}个视频");
            await Console.Out.WriteLineAsync($"共处理{count}个视频");

            logger.Info($"结束处理视频素材事务...");
            await Console.Out.WriteLineAsync("结束处理视频素材事务...");
        }

    }
}
