using System;
using System.Collections.Generic;
using System.Text;
using QRCoder;

namespace Ku.Core.Tools.QRCode
{
    public enum QRCodeLevel
    {
        H = QRCodeGenerator.ECCLevel.H,
        L = QRCodeGenerator.ECCLevel.L,
        M = QRCodeGenerator.ECCLevel.M,
        Q = QRCodeGenerator.ECCLevel.Q
    }

    public class QrCodeGen : QRCodeGenerator, IQrCodeGen
    {
        public byte[] Create(string data, QRCodeLevel level = QRCodeLevel.Q, int pixelsPerModule = 20)
        {
            QRCodeGenerator.ECCLevel lv = (QRCodeGenerator.ECCLevel)((int)level);
            var qrcodeData = CreateQrCode(data, lv);
            BitmapByteQRCode code = new BitmapByteQRCode(qrcodeData);
            return code.GetGraphic(pixelsPerModule);
        }

        public byte[] Create(string data, QRCodeLevel level, int pixelsPerModule, string darkColorHtmlHex, string lightColorHtmlHex)
        {
            QRCodeGenerator.ECCLevel lv = (QRCodeGenerator.ECCLevel)((int) level);
            var qrcodeData = CreateQrCode(data, lv);
            BitmapByteQRCode code = new BitmapByteQRCode(qrcodeData);
            return code.GetGraphic(pixelsPerModule, darkColorHtmlHex: darkColorHtmlHex, lightColorHtmlHex: lightColorHtmlHex);
        }
    }
}
