using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPreview.Media.Ocr;
using Windows.Graphics.Imaging;
using Abacus.Interfaces;
using XLabs.Platform.Services.Media;

namespace Abacus.WinPhone.Services
{
    class ParseImageWP : IParseImage
    {
        public string parsedText;

        public async Task<bool> parseTextFromImageASync(MediaFile file)
        {
            OcrResult result;
            UInt32 width, height;
            string recognizedText = "";

            OcrEngine ocrEngine = new OcrEngine(OcrLanguage.English);

            using (var stream = file.Source.AsRandomAccessStream())
            {
                // Create image decoder.
                var decoder = await BitmapDecoder.CreateAsync(stream);
                width = decoder.PixelWidth;
                height = decoder.PixelHeight;


                // Get pixels in BGRA format.
                var pixels = await decoder.GetPixelDataAsync(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Straight,
                    new BitmapTransform(),
                    ExifOrientationMode.RespectExifOrientation,
                    ColorManagementMode.ColorManageToSRgb);

                // Extract text from image.
                result = await ocrEngine.RecognizeAsync(height, width, pixels.DetachPixelData());

                // Check whether text is detected.
                if (result.Lines != null)
                {
                    // Collect recognized text.
                    foreach (var line in result.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            recognizedText += word.Text + " ";
                        }
                        recognizedText += Environment.NewLine;
                    }

                }
            }

            this.parsedText = recognizedText;

            return true;
        }

        public string returnParsedText()
        {
            return this.parsedText;
        }

    }
}
