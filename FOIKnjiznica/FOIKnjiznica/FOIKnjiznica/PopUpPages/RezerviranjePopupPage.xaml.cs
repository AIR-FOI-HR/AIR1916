using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOIKnjiznica.Classes;
using System.Net.Http;
using Newtonsoft.Json;

namespace FOIKnjiznica.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RezerviranjePopupPage : PopupPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        Classes.Publikacije publikacijeD;
        public RezerviranjePopupPage(Classes.Publikacije publikacijeU)
        {
            publikacijeD = publikacijeU;
            InitializeComponent();
            if (publikacijeD.Vrsta == "Slobodno")
            {
                Naziv.Text = publikacijeD.naziv;
                GumbRezerviraj.IsVisible = true;

            }
            else if (publikacijeD.Vrsta == "Rezervirano")
            {
                Naziv.Text = publikacijeD.naziv;
                GumbRezerviraj.IsVisible = false;
                Prikaz.Text = "Odabrana kopij je rezervirana!";
                DohvatiPublikaciju(publikacijeD.Kopija);
                //GumbPosudi.IsVisible = true; IMPLEMENTIRAT CE SE ZA POSUDBU JOS
            }
        }
        private async void izlazak_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void DohvatiPublikaciju(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "Rezervacije/" + id);
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            listaSvihPublikacija = publikacije;

            List<Classes.Publikacije> posljednjeStanjePublikacije = new List<Publikacije>();
            posljednjeStanjePublikacije.Add(listaSvihPublikacija[listaSvihPublikacija.Count - 1]);

            ListaPublikacije.ItemsSource = posljednjeStanjePublikacije;
            foreach (var item in listaSvihPublikacija)
            {
                item.GodinaOd = DateTime.Parse(item.GodinaOd).ToString();
                item.GodinaDo = DateTime.Parse(item.GodinaDo).ToString();
            }
        }

        public async void GumbRezervirajKliknut(object sender, EventArgs e)
        {
            PovijestPublikacije novoStanjePublikacije = new PovijestPublikacije() {datum = DateTime.Now, 
                                                                                   datum_do = DateTime.Now.AddDays(5), 
                                                                                   nazivPublikacije = publikacijeD.naziv,
                                                                                   nazivStatusa = "Rezervirano",
                                                                                   kopijaId = publikacijeD.Kopija,
                                                                                   clanoviId = Clanovi.id,
                                                                                   vrsta_statusaId = 3};

            HttpClient client = new HttpClient();
            var Json = JsonConvert.SerializeObject(novoStanjePublikacije);
            var content = new StringContent(Json, Encoding.UTF8, "application/json");
            var request = await client.PutAsync(WebServisInfo.PutanjaWebServisa + "GumbRezerviraj", content);

            var response = await request.Content.ReadAsStringAsync();
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);

            listaSvihPublikacija = publikacije;
            ListaPublikacije.ItemsSource = listaSvihPublikacija;
            MessagingCenter.Send<App>((App)Application.Current, "RezervacijaPublikacije");
            await PopupNavigation.Instance.PopAsync();
        }
        

    }
}