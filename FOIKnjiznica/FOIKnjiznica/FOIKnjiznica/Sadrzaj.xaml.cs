using FOIKnjiznica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sadrzaj : ContentPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        Publikacije publikacije;
        public Sadrzaj(Publikacije sadrzaj)
        {
            publikacije = sadrzaj;
            InitializeComponent();
            NaslovSadrzaja.Text = DohvatiNaziv(publikacije);
            webView.Source = DohvatiLink(publikacije);
        }


        private string DohvatiLink(Publikacije odabranaPublikacija)
        {
            string link = odabranaPublikacija.sadrzaj;
            return link;
        }
        private string DohvatiNaziv(Publikacije odabranaPublikacija)
        {
            string naziv = odabranaPublikacija.naziv;
            return naziv;
        }

        public async void PomocKliknuta(object sender, EventArgs e)
        {

        }
    }
}