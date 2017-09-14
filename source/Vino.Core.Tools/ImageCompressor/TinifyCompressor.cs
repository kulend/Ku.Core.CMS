using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinifyAPI;

namespace Vino.Core.Tools.ImageCompressor
{
    public class TinifyCompressor : IImageCompressor
    {
        public TinifyCompressor()
        {
            Tinify.Key = "l7CaDmXuUJA4ThnSup5gWVrQGT1GiNxn";
        }

        /// <summary>
        /// 压缩图片
        /// </summary>
        public async Task Compress(string filePath, string savePath)
        {
            var source = Tinify.FromFile(filePath);
            await source.ToFile(savePath);
        }
    }
}
