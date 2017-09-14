using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.Tools.ImageCompressor
{
    public interface IImageCompressor
    {
        Task Compress(string filePath, string savePath);
    }
}
