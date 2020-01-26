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

namespace FOIKnjiznica.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrekidRezervacijePopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        PovijestPublikacije rezervacija;
        public PrekidRezervacijePopupPage(PovijestPublikacije rezerviranaPublikacija)
        {
            InitializeComponent();

            TekstPrekidaKorisniku.Text = rezerviranaPublikacija.nazivPublikacije;

            rezervacija = rezerviranaPublikacija;
        }

        private async void NazadPritisnuto(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void PotvrdiPritisnuto(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var Json = JsonConvert.SerializeObject(rezervacija);
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "PrekidRezervacije", content);

            await httpClient.GetAsync("https://foiknjiznicaazurefunkcije.azurewebsites.net/api/OslobodenjeKnjigeNotifikacija?code=FE9IUHyiPZcyzcca7DEzTlBRVWh5HUElZIeLX4UE6JA73cPdoTyP9A==&idPublikacije=" + rezervacija.nazivPublikacije);

            await PopupNavigation.Instance.PopAsync();
        }
    }
}