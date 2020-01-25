using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Security.Cryptography;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinPostavljanje : ContentPage
    {
        private List<string> pinNumber = new List<string>();
        private int brojac = 0;
        private int ponovljeniPin;
        private int ispravnibroj = 0;
        private SHA256 sha256;
        private bool noviPin = true;
        private string pinIzBaze = "";
        
        public PinPostavljanje(bool postoji)
        {
            InitializeComponent();
            sha256 = SHA256.Create();
            if (postoji)
            {
                Naslov.Text = "Unesite novi PIN za promjenu";
                noviPin = false;
            }
            else
            {
                noviPin = true;
                Naslov.Text = "Unesite PIN";
            }
        }
        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new Profil();
            return base.OnBackButtonPressed();
        }

        private void DodajBroj(string broj)
        {
            brojac++;
            if (brojac == 1)
            {
                GumbUnos1.BackgroundColor = Color.FromHex("#FD8638");
                pinNumber.Add(broj);
            }
            else if (brojac == 2)
            {
                GumbUnos2.BackgroundColor = Color.FromHex("#FD8638");
                pinNumber.Add(broj);
            }
            else if (brojac == 3)
            {
                GumbUnos3.BackgroundColor = Color.FromHex("#FD8638");
                pinNumber.Add(broj);
            }
            else if (brojac == 4)
            {
                GumbUnos4.BackgroundColor = Color.FromHex("#FD8638");
                pinNumber.Add(broj);
                System.Threading.Thread.Sleep(400);
                ProvjeriIspravnostPina();
                brojac = 0;
                GumbUnos1.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos2.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos3.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos4.BackgroundColor = Color.FromHex("#FFFFFF");
            }
        }
        private void BtnJedan(object sender, EventArgs e)
        {
            DodajBroj("1");
        }
        private void BtnDva(object sender, EventArgs e)
        {
            DodajBroj("2");
        }
        private void BtnTri(object sender, EventArgs e)
        {
            DodajBroj("3");
        }
        private void BtnCetiri(object sender, EventArgs e)
        {
            DodajBroj("4");
        }
        private void BtnPet(object sender, EventArgs e)
        {
            DodajBroj("5");
        }
        private void BtnSest(object sender, EventArgs e)
        {
            DodajBroj("6");
        }
        private void BtnSedam(object sender, EventArgs e)
        {
            DodajBroj("7");
        }
        private void BtnOsam(object sender, EventArgs e)
        {
            DodajBroj("8");
        }
        private void BtnDevet(object sender, EventArgs e)
        {
            DodajBroj("9");
        }
        private void BtnNula(object sender, EventArgs e)
        {
            DodajBroj("0");
        }
        private void BtnDelete(object sender, EventArgs e)
        {
            IzbrisiPrethodnuBrojku();
        }
        private void BtnBack(object sender, EventArgs e)
        {
            App.Current.MainPage = new Profil();
        }
    }
}