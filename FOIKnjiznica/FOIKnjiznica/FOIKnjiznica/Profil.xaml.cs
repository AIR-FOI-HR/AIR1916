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

            emailKorisnikaLabela.Text = Classes.Clanovi.hrEduPersonUniqueID;
            mobitelKorisnikaLabela.Text = Classes.Clanovi.mobitelID;

            KreirajStatistiku();
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
        private async void GumbPovijest(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PovijestKorisnika());
        }


        public async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PostavkePrijaveModulom());

        public async void PomocKliknuta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.PomocPopUpPage("DOBRODOŠLI U POMOĆ ZA PREGLED VAŠEG PROFILA!", "Prikazan Vam je uvid u Vaše podatke - ime, prezime, e-mail i mobitel. Prikazana je statistika korištenja aktivnosti knjižnice te je omogućen uvid u sve aktivnosti. Vidljiv je i Vaš najraniji istek rezervacije te posudbe tako da lako znate koliko vam isti još vrijede."));
        }
    }
}