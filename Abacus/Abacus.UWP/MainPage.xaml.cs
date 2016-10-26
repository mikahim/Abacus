using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using Abacus.UWP.Services;
using Abacus.Interfaces;

namespace Abacus.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            IParseImage ipi = new ParseImageUWP();

            var container = new SimpleContainer();
            container.Register<IDevice>(t=> WindowsPhoneDevice.CurrentDevice);
            container.Register<IMediaPicker, MediaPicker>();
            container.Register<IParseImage>(t => ipi);

            Resolver.SetResolver(container.GetResolver());

            LoadApplication(new Abacus.App());
        }
    }
}
