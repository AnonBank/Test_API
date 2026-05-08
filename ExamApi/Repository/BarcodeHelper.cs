
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

namespace ExamApi.Repository
{
    public static class BarcodeHelper
    {
        public static bool Validate(string code)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^[A-Z0-9]{4}(-[A-Z0-9]{4}){3}$");
            return regex.IsMatch(code);
        }

        public static string GenerateCode39(string text)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_39,
                Options = new EncodingOptions
                {
                    Height = 80,
                    Width = 300,
                    Margin = 5
                }
            };

            var pixelData = writer.Write(text);

            using var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppRgb);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(
                    pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}