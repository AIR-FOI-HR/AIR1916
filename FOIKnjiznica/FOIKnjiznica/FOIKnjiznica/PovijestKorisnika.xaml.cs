using FOIKnjiznica.Classes;
using FOIKnjiznica.PopUpPages;
using Newtonsoft.Json;
using Plugin.Toast;
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
    public partial class PovijestKorisnika : ContentPage
    {
        public List<PovijestPublikacije> povijestPosudbi;
        public List<PovijestPublikacije> povijestPosudbiRezervirano;
        public List<PovijestPublikacije> povijestPosudbiPosudeno;
        public List<PovijestPublikacije> povijestPosudbiVraceno;
        public List<PovijestPublikacije> povijestPosudbiVracenoTemp;
        public PovijestKorisnika()
        {
            InitializeComponent();

            BindingContext = this;

            povijestPosudbiRezervirano = new List<PovijestPublikacije>();
            povijestPosudbiVracenoTemp = new List<PovijestPublikacije>();
            povijestPosudbiPosudeno = new List<PovijestPublikacije>();
            povijestPosudbiVraceno = new List<PovijestPublikacije>();

            DohvatiPovijest();
        }

        private void SortirajPovijest()
        {
            povijestPosudbiRezervirano.Clear();
            povijestPosudbiPosudeno.Clear();
            povijestPosudbiVraceno.Clear();

            foreach (PovijestPublikacije trenutnaPovijest in povijestPosudbi)
            {
                if (trenutnaPovijest.datum_do < DateTime.Now)
                {
                    trenutnaPovijest.bojaPozadine = Color.FromHex("dea7a7");
                }
                else if (trenutnaPovijest.datum_do == DateTime.Now)
                {
                    trenutnaPovijest.bojaPozadine = Color.FromHex("ffeb7f");
                }
                else
                {
                    trenutnaPovijest.bojaPozadine = Color.FromHex("c4f6b9");
                }

                if (trenutnaPovijest.nazivStatusa == "Rezervirano")
                {
                    povijestPosudbiRezervirano.Add(trenutnaPovijest);
                }
                else if (trenutnaPovijest.nazivStatusa == "Posudeno")
                {
                    povijestPosudbiPosudeno.Add(trenutnaPovijest);
                }
                else
                {
                    povijestPosudbiVracenoTemp.Add(trenutnaPovijest);
                }
            }

            povijestPosudbiRezervirano = povijestPosudbiRezervirano.OrderByDescending(stavka => stavka.datum).ToList<PovijestPublikacije>();
            povijestPosudbiPosudeno = povijestPosudbiPosudeno.OrderByDescending(stavka => stavka.datum).ToList<PovijestPublikacije>();

            povijestPosudbiVracenoTemp = povijestPosudbiVracenoTemp.OrderByDescending(stavka => stavka.datum).ToList<PovijestPublikacije>();
            foreach (PovijestPublikacije stavkaPovijesti in povijestPosudbiVracenoTemp.ToList())
            {
                if(povijestPosudbiPosudeno.Exists(x => x.datum_do.Date == stavkaPovijesti.datum.Date))
                {
                    povijestPosudbiVraceno.Add(stavkaPovijesti);
                }
            }

            StavkePovijestiRezervacije.ItemsSource = povijestPosudbiRezervirano;
            StavkePovijestiPosudbe.ItemsSource = povijestPosudbiPosudeno;
            StavkePovijestiVraceno.ItemsSource = povijestPosudbiVraceno;
        }

        private async void DohvatiPovijest()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "PovijestPosudbi/" + Clanovi.id);
            var publikacije = JsonConvert.DeserializeObject<List<PovijestPublikacije>>(response);
            povijestPosudbi = publikacije;
            client.Dispose();

            SortirajPovijest();
        }

        private async void PritisakRezerviranePublikacije(object sender, ItemTappedEventArgs e)
        {
            PovijestPublikacije pritisnutaPublikacija = e.Item as PovijestPublikacije;

            if (pritisnutaPublikacija.datum_do <= DateTime.Now)
            {
                CrossToastPopUp.Current.ShowCustomToast($"Ne možete prekinuti rezervaciju koja istekla ili ističe danas", "#ae2323", "White");
            }
            else
            {
                await PopupNavigation.PushAsync(new PrekidRezervacijePopupPage(pritisnutaPublikacija));
            }
        }

        public async void PomocKliknuta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.PomocPopUpPage("DOBRODOŠLI U POMOĆ ZA PREGLED POVIJESTI KORISNIKA!", "Prikazan Vam je uvid u sve vaše rezervacije, posudbe i vraćene knjige. Informacije su Vam prikazane po nazivu knjige, datumu kada ste istu posudili ili rezervirali te datumu kada Vam rezervacija ili posudba ističe. Kod vraćanja knjige prikazan Vam je njen naziv i kada ste istu vratili."));

        }
    }
}