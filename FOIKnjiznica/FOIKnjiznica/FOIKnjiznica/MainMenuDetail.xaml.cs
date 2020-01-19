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
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using FOIKnjiznica.Classes;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

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
            Classes.Clanovi.DohvatiFavorite();

            //Listener koji prima događaj od popup prozora te osvjezava listu
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjeAZ", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjeZA", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjePoGodini", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "sortiranjePoAutoru", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "filtriranjePublikacija", (sender) => { OsvjeziListuPublikacija(); });
            MessagingCenter.Subscribe<App>((App)Application.Current, "resetiranjeFiltera", (sender) => { OsvjeziListuPublikacija(); });

            AppCenter.Start("65068ce7-ba56-49b6-bc9e-03c39018c505", typeof(Push));
        }

        //Dohvacanje Publikacije za prikaz na zaslonu
        private async void DohvatiPublikacije()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "Publikacije");
                var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
                listaSvihPublikacija = publikacije;
                ListaPublikacije.ItemsSource = publikacije;

            }
            catch (Exception socketException) when (socketException is System.Net.Sockets.SocketException || socketException is HttpRequestException)
            {
                Console.WriteLine(socketException);

                await PopupNavigation.PushAsync(new PopUpPages.InternetObavijestPopupPage());

                DohvatiPublikacijeKadJeMoguce();
            }
            finally
            {
                client.Dispose();
            }
        }


        // Metoda koja pokušava dohvatiti publikacije tako dugo dok ne uspije, to jest tako dugo dok se korisnik ne spoji na internet
        private async void DohvatiPublikacijeKadJeMoguce()
        {
            bool dohvaceno = false;

            while (dohvaceno == false)
            {              
                HttpClient client = new HttpClient();
                try
                {
                    var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "Publikacije");
                    var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
                    listaSvihPublikacija = publikacije;
                    ListaPublikacije.ItemsSource = publikacije;
                    dohvaceno = true;

                }
                catch (Exception socketException) when (socketException is System.Net.Sockets.SocketException || socketException is HttpRequestException)
                {
                    Console.WriteLine(socketException);
                    dohvaceno = false;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        private async void UnosPretrazivanja(object sender, TextChangedEventArgs e)
        {
            string id = search_bar.Text.ToString();
            id = e.NewTextValue;
            if (id.Length > 0)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "PretragaPublikacija/" + id);
                var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
                ListaPublikacije.ItemsSource = publikacije;
            }
            else
            {
                DohvatiPublikacije();
            }
        }

        private async void filter_button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new IzbornikFiltracije());
        }
        
        private void OsvjeziListuPublikacija()
        {
            ListaPublikacije.ItemsSource = Classes.Filtar.FiltrirajPublikacije(listaSvihPublikacija);
        }

        private async void sort_button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.SortiranjePopupPage());
        }

        public async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Classes.Publikacije tappedItem = e.Item as Classes.Publikacije;
            await Navigation.PushAsync(new BookInfo(tappedItem));
        }
    }
}