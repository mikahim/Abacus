using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Abacus
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        void calcBtn_Clicked(object sender, EventArgs e)
        {
            //sivu aina uudeksi main pageksi - ei sinällään tarvetta stäkkinavigoinnille eikä jaksa miettiä mitä tapahtuu jos painaa backbuttonia kesken skannauksen =)
            App.Current.MainPage = new CalcPage();
        }

    }
}
