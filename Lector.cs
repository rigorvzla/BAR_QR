using AForge.Video.DirectShow;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using ZXing;
using BarcodeReader = ZXing.BarcodeReader;

namespace BAR_QR
{
    public class Lector
    {
        static public string DecoderImageFromFile(string file)
        {
            var bitmap = new Bitmap(file);
            var reader = new BarcodeReader();
            var result = reader.Decode(bitmap);

            if (result != null)
            {
                return result.Text;
            }

            return string.Empty;
        }

        static public async Task<string> DecodeImageFromCam()
        {
            BarcodeReader barcodeReader = new BarcodeReader();

            barcodeReader.Options = new ZXing.Common.DecodingOptions
            {
                PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.CODE_39
                }
            };

            TaskCompletionSource<string> completionSource = new TaskCompletionSource<string>();

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString); //seleccionar webcam

            videoSource.NewFrame += (sender, eventArgs) =>
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                Result result = barcodeReader.Decode(bitmap);

                if (result != null)
                {
                    completionSource.SetResult(result.Text);
                    videoSource.Stop(); // Detener la captura después de obtener el resultado
                }
            };

            videoSource.Start();

            return await completionSource.Task;
        }

    }
}
