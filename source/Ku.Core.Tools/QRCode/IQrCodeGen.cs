using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Tools.QRCode
{
    public interface IQrCodeGen
    {
        byte[] Create(string data, QRCodeLevel level = QRCodeLevel.Q, int pixelsPerModule = 20);

        byte[] Create(string data, QRCodeLevel level, int pixelsPerModule, string darkColorHtmlHex,
            string lightColorHtmlHex);
    }
}
