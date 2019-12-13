using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using FOIKnjiznica.PopUpPages;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookInfo : ContentPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        Publikacije publikacijeD;
        public BookInfo(Publikacije publikacijeU)
        {
            publikacijeD = publikacijeU;
            InitializeComponent();
            Naziv.Text = publikacijeD.naziv;
            Image.Source = publikacijeD.slika_url;
            Autor.Text = "Autor: " + publikacijeD.Autor;
            Isbn.Text = "ISBN: " + publikacijeD.isbn;
            Udk.Text = "UDK: " + publikacijeD.udk;
            Signatura.Text = "Signatura: " + publikacijeD.signatura;
            Jezik.Text = "Jezik: " + publikacijeD.jezik;
            Stranice.Text = "Stranice: " + publikacijeD.stranice.ToString();
            Godina.Text = "Godina izdanja: " + publikacijeD.godina_izdanja.ToString();
            Izdanje.Text = "Izdanje: " + publikacijeD.izdanje;
            Izdavac.Text = "Izdavac: " + publikacijeD.Izdavac;
            DohvatiPublikaciju(publikacijeD.id);
            MessagingCenter.Subscribe<App>((App)Application.Current, "RezervacijaPublikacije", (sender) => { OsvjeziListuPublikacija(); });
        }

        private void OsvjeziListuPublikacija()
        {
            DohvatiPublikaciju(publikacijeD.id);
        }

        private async void DohvatiPublikaciju(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Publikacije/"+id);
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            listaSvihPublikacija = publikacije;
            ListaPublikacije.ItemsSource = listaSvihPublikacija;
        }

        public async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Classes.Publikacije tappedItem = e.Item as Classes.Publikacije;
            await PopupNavigation.PushAsync(new RezerviranjePopupPage(tappedItem));
        }
    }
}