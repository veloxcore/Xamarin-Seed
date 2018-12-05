using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSeed.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://xamarin.com/platform"));
        }
    }
}