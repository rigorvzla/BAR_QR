using System.Drawing.Imaging;
using ZXing;

namespace BAR_QR
{
    public class Generador
    {
        public class Code_QR
        {
            static public void ImageQR(string text, string filename)
            {
                var barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                barcodeWriter.Options.Width = 200;
                barcodeWriter.Options.Height = 200;

                var barcodeBitmap = barcodeWriter.Write(text);
                barcodeBitmap.Save(filename, ImageFormat.Png);
            }
        }

        public class Code_BAR
        {
            static public void ImageBAR(string text, string filename)
            {
                var barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.CODE_39;
                barcodeWriter.Options.Width = 200;
                barcodeWriter.Options.Height = 200;

                var barcodeBitmap = barcodeWriter.Write(text);
                barcodeBitmap.Save(filename, ImageFormat.Png);
            }
        }
    }
}
