using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;  // You might need to install this package if not already present
using ZXing;
namespace Event_Management_System.Models
{
    public class QRCodeGenerator
    {
        public static byte[] GenerateQRCode(object data)
        {
            // Convert data object to JSON string
            var json = JsonConvert.SerializeObject(data);

            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = 230,
                Height = 230
            };

            using (Bitmap bitmap = barcodeWriter.Write(json))
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}