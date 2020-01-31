using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InterfaceModule;
using PINModul;

namespace FOIKnjiznica
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Provjera ako se član prijavio sa FOI prijavom da se otvori aplikacija, a ne nova prijava
            if (Classes.Clanovi.id != 0)
            {
                MainPage = new NavigationPage(new MainMenu());
            }
            else
            {
                MainPage = new NavigationPage(new StranicaPrijave());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
