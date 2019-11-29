using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using FOIKnjiznicaWebServis.Models;
using FOIKnjiznicaWebServis.Controllers;
using Rg.Plugins.Popup.Services;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuDetail : ContentPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        public MainMenuDetail()
        {
            InitializeComponent();
            BindingContext = this;
            DohvatiPublikacije();

            //Listener koji prima događaj od popup prozora te osvjezava listu
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjeAZ", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjeZA", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjePoGodini", (sender) => { OsvjeziListuPublikacija(); });
        }

        //Dohvacanje Publikacije za prikaz na zaslonu
        private async void DohvatiPublikacije()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Publikacije");
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            listaSvihPublikacija = publikacije;
            ListaPublikacije.ItemsSource = publikacije;
        }

        private async void UnosPretrazivanja(object sender, TextChangedEventArgs e)
        {
            string id = search_bar.Text.ToString();
            id = e.NewTextValue;
            if (id.Length > 0)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/PretragaPublikacija/"+id);
                var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
                ListaPublikacije.ItemsSource = publikacije;
            }
            else
            {
                DohvatiPublikacije();
            }
        }

        private void OsvjeziListuPublikacija()
        {
            ListaPublikacije.ItemsSource = listaSvihPublikacija;
        }

        private async void sort_button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.SortiranjePopupPage());
        }
    }
}