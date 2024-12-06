using SixLabors.ImageSharp.Formats.Bmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QRCoder;

namespace BarCode_sample.QRCoder
{
    public class QRCode_Sample
    {
        public class QrSpecsDto
        {
            public string Content { get; set; }
            public int Size { get; set; } = 15;
            public bool? Watermark { get; set; } = true;
            public int QRVersion { get; set; } = 5;
        }

        public async Task<byte[]> GenerateQR(QrSpecsDto specs)
        {
            QRCodeData qrCodeData;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                //https://blog.qr4.nl/page/QR-Code-Data-Capacity.aspx
                //How much information can a QR code have ?
                int stringBites = System.Text.ASCIIEncoding.ASCII.GetByteCount(specs.Content) * 8;
                switch (stringBites)
                {
                    case <= 100:
                        specs.QRVersion = 1;
                        break;
                    case <= 272:
                        specs.QRVersion = 2;
                        break;
                    case <= 440:
                        specs.QRVersion = 3;
                        break;
                    case <= 640:
                        specs.QRVersion = 4;
                        break;
                    case <= 864:
                        specs.QRVersion = 5;
                        break;
                    case <= 2192:
                        specs.QRVersion = 10;
                        break;
                    case <= 4184:
                        specs.QRVersion = 15;
                        break;
                    case <= 6888:
                        specs.QRVersion = 20;
                        break;
                    case <= 10208:
                        specs.QRVersion = 25;
                        break;
                    case <= 13880:
                        specs.QRVersion = 30;
                        break;
                    case <= 18448:
                        specs.QRVersion = 35;
                        break;
                    default:
                        specs.QRVersion = 40;
                        break;
                }

                qrCodeData = qrGenerator.CreateQrCode(specs.Content, QRCodeGenerator.ECCLevel.L, true, true, QRCodeGenerator.EciMode.Utf8, specs.QRVersion);
            }

            var webClient = new WebClient();
            byte[] imageBytes = webClient.DownloadData("https://componentesui.blob.core.windows.net/recursos/logos-gla/favicon.png");
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            var darkColor = SixLabors.ImageSharp.Color.Black;
            var lightColor = SixLabors.ImageSharp.Color.White;
            var iconBackgroundColor = SixLabors.ImageSharp.Color.Transparent;
            SixLabors.ImageSharp.Image qrIcon = SixLabors.ImageSharp.Image.Load(imageBytes);
            var QrCode = new QRCode(qrCodeData);
            if ((bool)specs.Watermark)
            {
                SixLabors.ImageSharp.Image qrCodeBitmap = QrCode.GetGraphic(specs.Size, darkColor, lightColor, qrIcon, 20, 0, true, iconBackgroundColor);
                using (var msBitmap = new MemoryStream())
                {
                    qrCodeBitmap.Save(msBitmap, new BmpEncoder());
                    return msBitmap.ToArray();
                }
            }
            else
            {
                SixLabors.ImageSharp.Image qrCodeBitmap = QrCode.GetGraphic(specs.Size, darkColor, lightColor, true);

                using (var msBitmap = new MemoryStream())
                {
                    qrCodeBitmap.Save(msBitmap, new BmpEncoder());
                    return msBitmap.ToArray();
                }
            }
        }
    }
}
