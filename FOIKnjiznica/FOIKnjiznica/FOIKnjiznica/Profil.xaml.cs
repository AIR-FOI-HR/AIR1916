using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profil : ContentPage
    {
        public Profil()
        {
            InitializeComponent();

            BindingContext = this;

            imeKorisnikaLabela.Text = Classes.Clanovi.ime;
            prezimeKorisnikaLabela.Text = Classes.Clanovi.prezime;
            emailKorisnikaLabela.Text = Classes.Clanovi.hrEduPersonUniqueID;
            mobitelKorisnikaLabela.Text = Classes.Clanovi.mobitelID;

        }

        private async void GumbPovijest(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PovijestKorisnika());
        }
    }
}