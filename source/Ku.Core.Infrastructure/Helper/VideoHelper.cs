using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Ku.Core.Infrastructure.Helper
{
    public class VideoHelper
    {
        private const string ffmpegPath = "C:\\ffmpeg-4.1-win64-static\\bin\\ffmpeg.exe";

        #region 从视频画面中截取一帧画面为图片

        /// <summary>
        /// 从视频画面中截取一帧画面为图片
        /// </summary>
        /// <returns></returns>
        public static string GetPreviewImage(string videoPath, string imgPath, int width = 320, int height = 180, int CutTimeFrame = 1)
        {
            imgPath = imgPath ?? videoPath.Substring(0, videoPath.LastIndexOf(".")) + $"_thumb.jpg";//视频图片的名字，绝对路径

            using (System.Diagnostics.Process ffmpeg = new System.Diagnostics.Process())
            {
                ffmpeg.StartInfo.UseShellExecute = false;
                ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                ffmpeg.StartInfo.RedirectStandardError = true;
                ffmpeg.StartInfo.FileName = ffmpegPath;
                ffmpeg.StartInfo.Arguments = $" -i \"{videoPath}\" -y -f image2 -ss {CutTimeFrame} -t 0.001 -s {width}*{height} \"{imgPath}\"";

                ffmpeg.Start();

                ffmpeg.WaitForExit();
            }
            if (File.Exists(imgPath))
                return imgPath;

            return null;
        }

        #endregion


        public static string GetVideoDuration(string videoPath)
        {
            using (System.Diagnostics.Process ffmpeg = new System.Diagnostics.Process())
            {
                ffmpeg.StartInfo.UseShellExecute = false;
                ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                ffmpeg.StartInfo.RedirectStandardError = true;
                ffmpeg.StartInfo.FileName = ffmpegPath;

                ffmpeg.StartInfo.Arguments = $"-i \"{videoPath}\"";

                ffmpeg.Start();
                var errorreader = ffmpeg.StandardError;
                ffmpeg.WaitForExit();

                var result = errorreader.ReadToEnd();

                if (result.Contains("Duration:"))
                {
                    return result.Substring(result.IndexOf("Duration: ") + 10, 8);
                }
                return null;
            }
        }
    }
}
