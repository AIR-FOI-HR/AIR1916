using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FaulandCc.XF.GesturePatternView;
using InterfaceModule;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UzorakModul
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UzorakUI : ContentPage
    {
        public Action<Type> zatvori;
        public SHA256 sha256;
        public bool noviUzorak = false;
        public string staraLozinka = "";
        public int ispravniBroj = 0;
        public string ponovljeniUzorak = "";
        public bool provjeraStareLozinke;

        public UzorakUI() { }

        public UzorakUI(string hashiraniPodatak, Action<Type> zatvaranjeUI)
        {
            InitializeComponent();

            staraLozinka = hashiraniPodatak;

            zatvori = zatvaranjeUI;
        }

        private void MyGesturePatternView_OnGesturePatternCompleted(object sender, GesturePatternCompletedEventArgs e)
        {
            int n = e.GesturePatternValue.Length;
            if (ProvjeriVelicinuUnosa(n))
            {
                if (ProvjeriPonavljanjeTocki(e.GesturePatternValue))
                {
                    IspravnostStarogUzorka(e.GesturePatternValue);
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

        private void IspravnostStarogUzorka(string uzorak)
        {
            string noviUzorakHash = HashirajUzorak(uzorak);
                if (noviUzorakHash == staraLozinka)
                {
                    zatvori(this.GetType());
                }
                else
                {
                    noviUzorak = false;
                    Obavijest.Text = " Uzorak je neispravan!";
                    OcistiUzorak();
                }
        }

        private string HashirajUzorak(string uzorak)
        {
            sha256 = SHA256.Create();
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
    }
}