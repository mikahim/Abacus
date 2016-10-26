using System;
using System.IO;
using Xamarin.Forms;
using Abacus.Interfaces;
using static Abacus.Services.StringCalculator;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace Abacus
{
    public partial class CalcPage : ContentPage
    {
        private IDevice device;
        private IMediaPicker mediaPicker;
        private IParseImage imageParser;

        public CalcPage()
        {
            InitializeComponent();
            device = Resolver.Resolve<IDevice>();
            mediaPicker = Resolver.Resolve<IMediaPicker>();
            imageParser = Resolver.Resolve<IParseImage>();
        }

        void MainPageBtn_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartPage();
        }

        async void ScanTextBtn_Clicked(object sender, EventArgs e)
        {
            scanTextBtn.IsEnabled = false;
            mainPageBtn.IsEnabled = false;
            resultLabel.Text = "Opening camera. Might take a while so please be patient!";
            originalPhoto.Source = "";

            //tesseractille whitelist sallituista merkeistä
            //tesseractApi.SetVariable("tessedit_char_whitelist", "012345789()/-+*");

            //kameran käyttöön optiot HUOM! pienentäminen ei toimi näissä kuin windowsilla
            var options = new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Rear };

            MediaFile picture = await mediaPicker.TakePhotoAsync(options);
            resultLabel.Text = "Processing image and recognizing text..";
            if (picture != null && picture.Source != null)
            {

                bool readSuccess = await imageParser.parseTextFromImageASync(picture);
                if (readSuccess)
                {
                    originalPhoto.Source = ImageSource.FromStream(() => picture.Source);

                    string calculation = imageParser.returnParsedText();

                    //tässä rajoitetaan laskutoimituksen pituus
                    if (calculation.Length <= 10)
                    {
                        //HUOM! tässä ei tarkisteta menevää stringiä mitenkään joten ohjelma kaatuu jos sinne pääsee mitäsattuu läpi
                        string result = CalculateString(calculation);
                        resultLabel.Text = calculation + " = " + result;
                    }

                    //jos ei saada järkevää stringiä luettua ts. siitä tulee aivan liian pitkä niin =>
                    else resultLabel.Text = "OCR result unclear, please try again.";
                }
            }
            scanTextBtn.IsEnabled = true;
            mainPageBtn.IsEnabled = true;
            scanTextBtn.Text = "Calculate again";

        }
    }
}
