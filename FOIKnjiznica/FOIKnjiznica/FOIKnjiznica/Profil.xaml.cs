using FOIKnjiznica.Classes;
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
    public partial class Profil : ContentPage
    {
        public StatistikaKorisnika statistikaTrenutnogKorisnika;
        private ClanoviAuthProtokol authProtokol = new ClanoviAuthProtokol();
        public List<ClanoviAuthProtokol> listaLozinki { get; set; }
        private string lozinka;
        private int odabranOdabirLozinke = 1;
        public Profil()
        {
            InitializeComponent();

            BindingContext = this;

            imeKorisnikaLabela.Text = Classes.Clanovi.ime;
            prezimeKorisnikaLabela.Text = Classes.Clanovi.prezime;
            emailKorisnikaLabela.Text = Classes.Clanovi.hrEduPersonUniqueID;
            mobitelKorisnikaLabela.Text = Classes.Clanovi.mobitelID;

            KreirajStatistiku();
            ProvjeriLozinke();

        }
        public Profil(int odabir)
        {
            InitializeComponent();

            BindingContext = this;

            imeKorisnikaLabela.Text = Classes.Clanovi.ime;
            prezimeKorisnikaLabela.Text = Classes.Clanovi.prezime;
            emailKorisnikaLabela.Text = Classes.Clanovi.hrEduPersonUniqueID;
            mobitelKorisnikaLabela.Text = Classes.Clanovi.mobitelID;

            KreirajStatistiku();
            ProvjeriLozinke();
            if(odabir == 1)
            {
                PinCheck.IsChecked = true;
                lblPinCheck.Text = "Dodano";
                CrossToastPopUp.Current.ShowCustomToast($"Uspješno ste dodali Pin opciju za bržu prijavu", "#ae2323", "White");
            }  
        }

        private async void KreirajStatistiku()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "Statistika/" + Classes.Clanovi.id);
            var publikacije = JsonConvert.DeserializeObject<Classes.StatistikaKorisnika>(response);
            statistikaTrenutnogKorisnika = publikacije;

            trenutniBrojRezervacija.Text = statistikaTrenutnogKorisnika.trenutniBrojRezervacija.ToString();
            trenutniBrojPosudbi.Text = statistikaTrenutnogKorisnika.trenutniBrojPosudba.ToString();
            ukupniBrojRezervacija.Text = statistikaTrenutnogKorisnika.ukupniBrojRezervacija.ToString();
            ukupniBrojPosudbi.Text = statistikaTrenutnogKorisnika.ukupniBrojPosudba.ToString();
            ukupniBrojPosudenihDana.Text = statistikaTrenutnogKorisnika.ukupniBrojPosudenihDana.ToString();
            ukupniBrojRezerviranihDana.Text = statistikaTrenutnogKorisnika.ukupniBrojRezerviranihDana.ToString();

            if(statistikaTrenutnogKorisnika.najranijiIstekPosudbeDatum == null || statistikaTrenutnogKorisnika.najranijiIstekPosudbeDatum <= DateTime.Now)
            {
                najranijiIstekPosudbeDatum.Text = "Datum: ";
                najranijiIstekPosudbeNaziv.Text = "Naziv: ";
            }
            else
            {
                najranijiIstekPosudbeDatum.Text = "Datum: ( " + statistikaTrenutnogKorisnika.najranijiIstekPosudbeDatum.ToString("dd/MM/yy") + " )";
                najranijiIstekPosudbeNaziv.Text = "Naziv: ( " + statistikaTrenutnogKorisnika.najranijiIstekPosudbeNaziv + " )";
            }

            if (statistikaTrenutnogKorisnika.najranijiIstekRezervacijeDatum == null || statistikaTrenutnogKorisnika.najranijiIstekRezervacijeDatum <= DateTime.Now)
            {
                najranijiIstekRezervacijeDatum.Text = "Datum: ";
                najranijiIstekRezervacijeNaziv.Text = "Naziv: ";
            }
            else
            {
                najranijiIstekRezervacijeDatum.Text = "Datum: ( " + statistikaTrenutnogKorisnika.najranijiIstekRezervacijeDatum.ToString("dd/MM/yy")+ " )";
                najranijiIstekRezervacijeNaziv.Text = "Naziv: ( " + statistikaTrenutnogKorisnika.najranijiIstekRezervacijeNaziv + " )";
            }


        }

        private async void ProvjeriLozinke()
        {
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "DodajPin/" + Clanovi.id);
                var odgovor = JsonConvert.DeserializeObject<List<Classes.ClanoviAuthProtokol>>(response);
                listaLozinki = odgovor;
            }
            catch (Exception socketException) when (socketException is System.Net.Sockets.SocketException || socketException is HttpRequestException)
            {

            }
            finally
            {
                client.Dispose();
            }
            if(listaLozinki != null)
            {
                foreach (var item in listaLozinki)
                {
                    authProtokol.Auth_ProtocolId = item.Auth_ProtocolId;
                    authProtokol.podaci = item.podaci;
                }

                if (authProtokol.Auth_ProtocolId == 2)
                {
                    UzorakPotvrdeno.IsChecked = true;
                    PinCheck.IsChecked = false;
                    OtisakPotvrdeno.IsChecked = false;
                    odabranOdabirLozinke = 2;
                    lozinka = authProtokol.podaci;
                }
                else if (authProtokol.Auth_ProtocolId == 3)
                {
                    UzorakPotvrdeno.IsChecked = false;
                    PinCheck.IsChecked = false;
                    OtisakPotvrdeno.IsChecked = true;
                    odabranOdabirLozinke = 3;
                }
                else if (authProtokol.Auth_ProtocolId == 4)
                {
                    UzorakPotvrdeno.IsChecked = false;
                    PinCheck.IsChecked = true;
                    OtisakPotvrdeno.IsChecked = false;
                    odabranOdabirLozinke = 4;
                    lozinka = authProtokol.podaci;
                }
            }
            else
            {
                UzorakPotvrdeno.IsChecked = false;
                PinCheck.IsChecked = false;
                OtisakPotvrdeno.IsChecked = false;
                odabranOdabirLozinke = 1;
            }
            
            
        }

        private async void GumbPovijest(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PovijestKorisnika());
        }

        private void GumbPin(object sender, EventArgs e)
        {
            if(odabranOdabirLozinke == 4)
            {
                App.Current.MainPage = new PinPostavljanje(true, lozinka, false);
            }
            else
            {
                if(lozinka == null)
                {
                    App.Current.MainPage = new PinPostavljanje(false, lozinka, false);
                }
                else
                {
                    if(odabranOdabirLozinke == 2)
                    {
                        App.Current.MainPage = new UzorakPostavljanje(false, lozinka, true);
                    }
                }
                
            }
            
        }
        private void GumbUzorak(object sender, EventArgs e)
        {
            if (odabranOdabirLozinke == 2)
            {
                App.Current.MainPage = new UzorakPostavljanje(true, lozinka, false);
            }
            else
            {
                if(lozinka == null)
                {
                    App.Current.MainPage = new UzorakPostavljanje(false, lozinka, false);
                }
                else
                {
                    if(odabranOdabirLozinke == 4)
                    {
                        App.Current.MainPage = new PinPostavljanje(false, lozinka, true);
                    }
                }
                
            }

        }

    }
}