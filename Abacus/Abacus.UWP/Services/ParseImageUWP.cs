using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Abacus.Interfaces;
using XLabs.Platform.Services.Media;

namespace Abacus.UWP.Services
{
    class ParseImageUWP : IParseImage
    {
        public string parsedText;

        public async Task<bool> parseTextFromImageASync(MediaFile file)
        {


            OcrResult result;

            OcrEngine ocrEngine = OcrEngine.TryCreateFromLanguage(new Language("en"));

            using (var stream = file.Source.AsRandomAccessStream())
            {
                // Create image decoder.
                var decoder = await BitmapDecoder.CreateAsync(stream);

                // Load bitmap.
                var bitmap = await decoder.GetSoftwareBitmapAsync();

                // Extract text from image.
                result = await ocrEngine.RecognizeAsync(bitmap);

            }

            this.parsedText = result.Text;

            return true;

        }

        public string returnParsedText()
        {
            return this.parsedText;
        }
    }
}
