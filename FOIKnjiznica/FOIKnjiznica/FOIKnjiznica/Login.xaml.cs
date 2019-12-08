using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Fingerprint;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            //Skrivanje Navigation Bar-a sa početnog zaslona "Main Menu" koji je ujedno i root navigacije
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            // Postavljanje Background slike
            BackgroundImageSource = "loginBackground";
        }

        //Login putem Fingerprinta
        private async void login_button_Clicked(object sender, EventArgs e)
        {
            var result = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Touch your fingerprint sensor");
                if (auth.Authenticated)
                {
                    await Navigation.PushAsync(new MainMenu());
                }
                else
                {
                    await DisplayAlert("Authentication Failed", "Fingerprint authentication failed", "OK");
                }
            }
        }
    }
}