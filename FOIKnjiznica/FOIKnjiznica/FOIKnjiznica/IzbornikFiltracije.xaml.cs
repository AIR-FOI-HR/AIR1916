using Newtonsoft.Json;
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
    public partial class IzbornikFiltracije : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ListView trenutniListView;
        public Button gumbTrenutnoAktivneKategorije;

        public IzbornikFiltracije()
        {
            InitializeComponent();
            BindingContext = this;

            DohvatiAutore();
            DohvatiIzdavace();
            DohvatiKategorije();
            DohvatiSlova();
        }

        // Dohvacanje liste s autorima kako bi korisnik mogao odabrati autore za filtriranje
        // Dohvaceni JSON se pretvara u listu tipa Autori te se ta lista dodaje kao izvor podataka 
        // za ListView kontrolu pod imenom FiltarAutora koji se moze vidjeti u xaml datoteci
        private async void DohvatiAutore()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Autori");
            var autori = JsonConvert.DeserializeObject<List<Classes.Autori>>(response);
            FiltarAutora.ItemsSource = autori;
        }

        //Dohvacanje liste izdavača na isti način kao što se dohvatila i lista autora
        private async void DohvatiIzdavace()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Izdavaci");
            var izdavaci = JsonConvert.DeserializeObject<List<Classes.Izdavaci>>(response);
            FiltarIzdavaca.ItemsSource = izdavaci;
        }

        //Kreiranje liste svih slova engleske abecede te postavljanje iste kao izvor podataka za ListView prikaz 
        //filtra slova
        private void DohvatiSlova()
        {
            List<Classes.Slova> slova = new List<Classes.Slova>();
            Classes.Slova slovo;

            for (int i = 0; i < 26; i++)
            {
                slovo = new Classes.Slova();

                slovo.slovo = (char)(i + 65);

                slova.Add(slovo);
            }

            FiltarSlova.ItemsSource = slova;
        }

        //Dohvaćanje liste kategorija te postavljanje liste kao izvor podataka za filtar kategorija 
        private async void DohvatiKategorije()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Kategorije");
            var kategorije = JsonConvert.DeserializeObject<List<Classes.Kategorije>>(response);
            FiltarKategorija.ItemsSource = kategorije;
        }


        //Prikazivanje trenutno odabranog filtra kada se pritisne na odgovarajući gumb, te zatvaranje ostalih filtara
        private void PrikazFiltra(ListView odabraniListView)
        {
            if (odabraniListView.IsVisible == false) 
            {
                if (trenutniListView != null)
                {
                    trenutniListView.IsVisible = false;
                }

                odabraniListView.IsVisible = true;

                trenutniListView = odabraniListView;
            }
            else
            {
                odabraniListView.IsVisible = false;
            }
            
        }

        private void OznaciKategoriju(Button odabraniGumb)
        {
            if(odabraniGumb == gumbTrenutnoAktivneKategorije)
            {
                odabraniGumb.TextColor = Color.FromHex("#ffffff");

                gumbTrenutnoAktivneKategorije = null;
            }
            else
            {
                if (gumbTrenutnoAktivneKategorije != null)
                {
                    gumbTrenutnoAktivneKategorije.TextColor = Color.FromHex("#ffffff");
                }

                odabraniGumb.TextColor = Color.FromHex("#000000");

                gumbTrenutnoAktivneKategorije = odabraniGumb;
            }

        }

        private void Autori_Clicked(object sender, EventArgs e)
        {
            PrikazFiltra(FiltarAutora);
            OznaciKategoriju(gumb_Autori);
        }

        private void Izdavaci_Clicked(object sender, EventArgs e)
        {
            PrikazFiltra(FiltarIzdavaca);
            OznaciKategoriju(gumb_Izdavaci);
        }

        private void Slovo_Clicked(object sender, EventArgs e)
        {
            PrikazFiltra(FiltarSlova);
            OznaciKategoriju(gumb_Slova);
        }

        private void Kategorije_Clicked(object sender, EventArgs e)
        {
            PrikazFiltra(FiltarKategorija);
            OznaciKategoriju(gumb_Kategorije);
        }
    }
}