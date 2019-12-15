using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using Plugin.Toast;
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
    public partial class BookInfo : ContentPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        Publikacije publikacijeD;

        bool jeFavorit = false;
        bool prvaProvjera = true;
        public string slikaFavorita { get; private set; }
        public BookInfo(Publikacije publikacijeU)
        {
            publikacijeD = publikacijeU;

            InitializeComponent();
            this.Disappearing += PosaljiPorukuOsvjezavanja;

            ProvjeriJeLiFavorit();

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
        }
        private async void DohvatiPublikaciju(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica.azurewebsites.net/api/Publikacije/"+id);
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            listaSvihPublikacija = publikacije;
            ListaPublikacije.ItemsSource = listaSvihPublikacija;
        }
        private async void ButtonSadrzaj(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Sadrzaj(publikacijeD));
        }

        private void ProvjeriJeLiFavorit()
        {
            jeFavorit = Classes.Clanovi.listaFavorita.Any(x => x.id == publikacijeD.id);

            if (jeFavorit)
            {
                //TODO: kreirati dodavanje favorita u bazu podataka 

                slikaFavorita = "jeFavorit.png";
                ZvijezdaFavorita.Source = slikaFavorita;

                if (prvaProvjera==false)
                {
                    CrossToastPopUp.Current.ShowCustomToast($"Uspješno ste dodali {publikacijeD.naziv} u favorite", "#ae2323","White");
                }

                prvaProvjera = false ;
            }
            else
            {
                //TODO: kreirati brisanje favorita iz baze podataka

                slikaFavorita = "nijeFavorit.png";
                ZvijezdaFavorita.Source = slikaFavorita;

                if (prvaProvjera==false)
                {
                    CrossToastPopUp.Current.ShowCustomToast($"Uspješno ste izbrisali {publikacijeD.naziv} iz favorita", "#ae2323", "White");
                }

                prvaProvjera = false;
            }

        }

        //TODO: Dodati u kontroler dodavanje i brisanje favorita te implementirati u metodi ispod

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            jeFavorit = Classes.Clanovi.listaFavorita.Any(x => x.id == publikacijeD.id);

            if (jeFavorit)
            {
                Classes.Clanovi.listaFavorita.RemoveAll(x => x.id == publikacijeD.id);
                ProvjeriJeLiFavorit();
            }
            else
            {
                Classes.Clanovi.listaFavorita.Add(publikacijeD);
                ProvjeriJeLiFavorit();
            }
        }

        private void PosaljiPorukuOsvjezavanja(object sender, EventArgs e)
        {
            MessagingCenter.Send<App>((App)Application.Current, "osvjeziFavorite");
        }


    }
}