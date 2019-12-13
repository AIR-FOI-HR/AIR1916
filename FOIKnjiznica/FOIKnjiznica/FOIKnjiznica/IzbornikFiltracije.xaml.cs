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
    public partial class IzbornikFiltracije : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ListView trenutniListView;
        public List<Classes.Autori> autori;
        public List<Classes.Kategorije> kategorije;
        public List<Classes.Izdavaci> izdavaci;
        public List<Classes.Slova> slova;
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
            autori = JsonConvert.DeserializeObject<List<Classes.Autori>>(response);
            ProvjeriPostojeceFiltreAutor();
            FiltarAutora.ItemsSource = autori;
            client.Dispose();
        }

        //Dohvacanje liste izdavača na isti način kao što se dohvatila i lista autora
        private async void DohvatiIzdavace()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Izdavaci");
            izdavaci = JsonConvert.DeserializeObject<List<Classes.Izdavaci>>(response);
            ProvjeriPostojeceFiltreIzdavac();
            FiltarIzdavaca.ItemsSource = izdavaci;
            client.Dispose();
        }

        //Kreiranje liste svih slova engleske abecede te postavljanje iste kao izvor podataka za ListView prikaz 
        //filtra slova
        private void DohvatiSlova()
        {
            slova = new List<Classes.Slova>();
            Classes.Slova slovo;

            for (int i = 0; i < 26; i++)
            {
                slovo = new Classes.Slova();

                slovo.slovo = (char)(i + 65);

                slova.Add(slovo);
            }
            ProvjeriPostojeceFiltreSlovo();
            FiltarSlova.ItemsSource = slova;
        }

        //Dohvaćanje liste kategorija te postavljanje liste kao izvor podataka za filtar kategorija 
        private async void DohvatiKategorije()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Kategorije");
            kategorije = JsonConvert.DeserializeObject<List<Classes.Kategorije>>(response);
            ProvjeriPostojeceFiltreKategorija();
            FiltarKategorija.ItemsSource = kategorije;
            client.Dispose();
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

        //Metode koje se aktiviraju kod pritiska na gumb neke kategorije
        //filtra. Metode zovu metode za prikaz odgovarajućeg ListViewa
        // te označavanje odabrane kategorije.
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


        //Metoda koja se izvršava kad korisnik pritisne na gumb za reset odabira.
        //Metoda je implementirana tako da se za svaku ListView kontrolu napravi
        //refresh te se dohvate podaci u slučaju neke promjene podataka
        private async void Reset_Clicked(object sender, EventArgs e)
        {
            Classes.Filtar.filtarAutori.Clear();
            Classes.Filtar.filtarIzdavaci.Clear();
            Classes.Filtar.filtarKategorije.Clear();
            Classes.Filtar.filtarSlova.Clear();

            FiltarAutora.BeginRefresh();
            DohvatiAutore();
            FiltarAutora.EndRefresh();

            FiltarIzdavaca.BeginRefresh();
            DohvatiIzdavace();
            FiltarIzdavaca.EndRefresh();

            FiltarKategorija.BeginRefresh();
            DohvatiKategorije();
            FiltarKategorija.EndRefresh();

            FiltarSlova.BeginRefresh();
            DohvatiSlova();
            FiltarSlova.EndRefresh();

            MessagingCenter.Send<App>((App)Application.Current, "resetiranjeFiltera");
            await PopupNavigation.Instance.PopAsync();
        }

        private async void Accept_Clicked(object sender, EventArgs e)
        {
            Classes.Filtar.filtarAutori.Clear();
            Classes.Filtar.filtarIzdavaci.Clear();
            Classes.Filtar.filtarKategorije.Clear();
            Classes.Filtar.filtarSlova.Clear();

            ListaOdabranihFiltra();

            MessagingCenter.Send<App>((App)Application.Current, "filtriranjePublikacija");

            await PopupNavigation.Instance.PopAsync();
        }

        private void ListaOdabranihFiltra()
        {
            foreach (var autor in autori)
            {
                if(autor.odabrano == true)
                {
                    Classes.Filtar.filtarAutori.Add(autor);
                }
            }

            foreach (var kategorija in kategorije)
            {
                if (kategorija.odabrano == true)
                {
                    Classes.Filtar.filtarKategorije.Add(kategorija);
                }
            }

            foreach (var izdavac in izdavaci)
            {
                if (izdavac.odabrano == true)
                {
                    Classes.Filtar.filtarIzdavaci.Add(izdavac);
                }
            }

            foreach (var slovo in slova)
            {
                if (slovo.odabrano == true)
                {
                    Classes.Filtar.filtarSlova.Add(slovo);
                }
            }
        }

        private void ProvjeriPostojeceFiltreAutor()
        {
            if(Classes.Filtar.filtarAutori.Count > 0)
            {
                foreach(var autor in Classes.Filtar.filtarAutori)
                {
                    foreach (var autorLista in autori)
                    {
                        if(autor.id == autorLista.id)
                        {
                            autorLista.odabrano = true;
                        }
                    }
                }
            }
        }

        private void ProvjeriPostojeceFiltreIzdavac()
        {
            if (Classes.Filtar.filtarIzdavaci.Count > 0)
            {
                foreach (var izdavac in Classes.Filtar.filtarIzdavaci)
                {
                    foreach (var izdavacLista in izdavaci)
                    {
                        if (izdavac.id == izdavacLista.id)
                        {
                            izdavacLista.odabrano = true;
                        }
                    }
                }
            }
        }

        private void ProvjeriPostojeceFiltreKategorija()
        {
            if (Classes.Filtar.filtarKategorije.Count > 0)
            {
                foreach (var kategorija in Classes.Filtar.filtarKategorije)
                {
                    foreach (var kategorijaLista in kategorije)
                    {
                        if (kategorija.id == kategorijaLista.id)
                        {
                            kategorijaLista.odabrano = true;
                        }
                    }
                }
            }
        }

        private void ProvjeriPostojeceFiltreSlovo()
        {
            if (Classes.Filtar.filtarSlova.Count > 0)
            {
                foreach (var slovo in Classes.Filtar.filtarSlova)
                {
                    foreach (var slovaLista in slova)
                    {
                        if (slovo.slovo == slovaLista.slovo)
                        {
                            slovaLista.odabrano = true;
                        }
                    }
                }
            }
        }

        private async void izlazak_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}