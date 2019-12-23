using FOIKnjiznica.Classes;
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
    public partial class Profil : ContentPage
    {
        public List<PovijestPublikacije> povijestPosudbi;
        public List<PovijestPublikacije> povijestPosudbiRezervirano;
        public List<PovijestPublikacije> povijestPosudbiPosudeno;
        public List<PovijestPublikacije> povijestPosudbiVraceno;
        public Profil()
        {
            InitializeComponent();

            BindingContext = this;

            povijestPosudbiRezervirano = new List<PovijestPublikacije>();
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
                if(trenutnaPovijest.datum_do <= DateTime.Now)
                {
                    trenutnaPovijest.bojaPozadine = Color.FromHex("dea7a7"); 
                }
                else
                {
                    trenutnaPovijest.bojaPozadine = Color.FromHex("c4f6b9");
                }

                if (trenutnaPovijest.nazivStatusa == "Rezervirano")
                {
                    povijestPosudbiRezervirano.Add(trenutnaPovijest);
                }
                else if(trenutnaPovijest.nazivStatusa == "Posudeno")
                {
                    povijestPosudbiPosudeno.Add(trenutnaPovijest);
                }
                else
                {
                    povijestPosudbiVraceno.Add(trenutnaPovijest);
                }
            }

            povijestPosudbiRezervirano = povijestPosudbiRezervirano.OrderBy(stavka => stavka.datum).ToList<PovijestPublikacije>();
            povijestPosudbiPosudeno = povijestPosudbiPosudeno.OrderBy(stavka => stavka.datum).ToList<PovijestPublikacije>();
            povijestPosudbiVraceno = povijestPosudbiVraceno.OrderBy(stavka => stavka.datum).ToList<PovijestPublikacije>();

            StavkePovijestiRezervacije.ItemsSource = povijestPosudbiRezervirano;
            StavkePovijestiPosudbe.ItemsSource = povijestPosudbiPosudeno;
            StavkePovijestiVraceno.ItemsSource = povijestPosudbiVraceno;
        }

        private async void DohvatiPovijest()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica1.azurewebsites.net/api/PovijestPosudbi/" + Clanovi.id);
            var publikacije = JsonConvert.DeserializeObject<List<PovijestPublikacije>>(response);
            povijestPosudbi = publikacije;
            client.Dispose();

            SortirajPovijest();
        }
    }
}