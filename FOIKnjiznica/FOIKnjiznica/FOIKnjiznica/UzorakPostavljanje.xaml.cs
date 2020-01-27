using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FaulandCc.XF.GesturePatternView;
using System.Security.Cryptography;
using Xamarin.Essentials;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UzorakPostavljanje : ContentPage
    {
        private SHA256 sha256;
        private bool noviUzorak = false;
        private string staraLozinka = "";
        private int ispravniBroj = 0;
        private string ponovljeniUzorak = "";
        private bool provjeraStareLozinke;
        public UzorakPostavljanje(bool postoji, string lozinka, bool provjera)
        {
            InitializeComponent();
            sha256 = SHA256.Create();
            provjeraStareLozinke = provjera;
            if (postoji && !provjeraStareLozinke)
            {
                Naslov.Text = "Unesite stari Uzorak za promjenu";
                noviUzorak = false;
                staraLozinka = lozinka;
            }else if (!postoji && provjeraStareLozinke)
            {
                Naslov.Text = "Unesite uzorak za provjeru";
                noviUzorak = false;
                staraLozinka = lozinka;
            }
            else
            {
                noviUzorak = true;
                Naslov.Text = "Unesite novi uzorak";
            }
        }
        private void MyGesturePatternView_OnGesturePatternCompleted(object sender, GesturePatternCompletedEventArgs e)
        {
            int n = e.GesturePatternValue.Length;
            if (ProvjeriVelicinuUnosa(n))
            {
                if (ProvjeriPonavljanjeTocki(e.GesturePatternValue))
                {
                    ProvjeriIspravnostUzorka(e.GesturePatternValue);
                }
                else
                {
                    Obavijest.Text = "Tocke se ne smiju ponavljati!";
                    OcistiUzorak();
                }
            }
            else
            {
                Obavijest.Text = "Predugi ili prektratki uzorak";
                OcistiUzorak();
            }
        }


        private void ProvjeriIspravnostUzorka(string uzorak)
        {
            if (noviUzorak)
            {
                IspravnostNovogUzorka(uzorak);
            }
            else
            {
                IspravnostStarogUzorka(uzorak);
            }
        }

        private void IspravnostNovogUzorka(string uzorak)
        {
            if (provjeraStareLozinke)
            {
                ispravniBroj++;
            }
            ispravniBroj++;
            if(ispravniBroj < 2)
            {
                ponovljeniUzorak = uzorak;
                OcistiUzorak();
                Obavijest.Text = "Ponovite uzorak";


            }else if(ispravniBroj == 2)
            {
                if(uzorak == ponovljeniUzorak)
                {
                    ispravniBroj = 0;
                    OcistiUzorak();
                    PohraniUzorakUBazu(uzorak);
                    Obavijest.Text = "Ispravan uzorak";
                }
                else
                {
                    ispravniBroj = 0;
                    Vibration.Vibrate();
                    Obavijest.Text = "Vaš ponovljeni uzorak se ne poklapa!";
                    OcistiUzorak();
                }
            }else if(ispravniBroj > 2)
            {
                ispravniBroj = 0;
                ispravniBroj = 0;
                Vibration.Vibrate();
                Obavijest.Text = "Vaš uzorak je netočan!";
                OcistiUzorak();
            }
        }

        private void IspravnostStarogUzorka(string uzorak)
        {
            string noviUzorakHash = HashirajUzorak(uzorak);
            if (provjeraStareLozinke)
            {
                if (noviUzorakHash == staraLozinka)
                {
                    OcistiUzorak();
                    App.Current.MainPage = new PinPostavljanje(false, null, false);
                }
                else
                {
                    noviUzorak = false;
                    Obavijest.Text = " Uzorak je neispravan!";
                    OcistiUzorak();
                }
            }
            else
            {
                if (noviUzorakHash == staraLozinka)
                {
                    noviUzorak = true;
                    Naslov.Text = "Unesite novi uzorak";
                    OcistiUzorak();
                }
                else
                {
                    noviUzorak = false;
                    Obavijest.Text = " Uzorak je neispravan!";
                    OcistiUzorak();
                }
            }
            
        }

        private void PohraniUzorakUBazu(string lozinka)
        {
            string uzorakHash = HashirajUzorak(lozinka);
        }

        private string HashirajUzorak(string uzorak)
        {
            byte[] uzorakByte = Encoding.UTF8.GetBytes(uzorak);
            byte[] izracunatiUzorakByte = sha256.ComputeHash(uzorakByte);
            string uzorakHash = Convert.ToBase64String(izracunatiUzorakByte);
            return uzorakHash;
        }

        private bool ProvjeriVelicinuUnosa(int n)
        {
            if (n > 9 || n < 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool ProvjeriPonavljanjeTocki(String e)
        {
            int unos = Int32.Parse(e);
            if (ProvjeriBroj(unos))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        private bool ProvjeriBroj(int n)
        {
            bool[] arr = new bool[10];

            for (int i = 0; i < 10; i++)
                arr[i] = false;

            while (n > 0)
            {
                int digit = n % 10;
                if (arr[digit])
                    return false;
                arr[digit] = true;
                n = n / 10;
            }
            return true;
        }

        private void OcistiUzorak()
        {
            this.MyGesturePatternView.Clear();
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new Profil();
            return base.OnBackButtonPressed();
        }
    }
}