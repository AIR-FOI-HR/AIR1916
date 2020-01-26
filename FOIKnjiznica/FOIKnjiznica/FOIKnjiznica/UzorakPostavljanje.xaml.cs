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
    public partial class UzorakPostavljanje : ContentPage
    {
        private SHA256 sha256;
        private bool noviPattern = false;
        private string staraLozinka = "";
        private int ispravniBroj = 0;
        private string ponovljeniUzorak = "";
        public UzorakPostavljanje(bool postoji, string lozinka)
        {
            InitializeComponent();
            if (postoji)
            {
                Naslov.Text = "Unesite stari Uzorak za promjenu";
                noviPattern = false;
                staraLozinka = lozinka;
            }
            else
            {
                noviPattern = true;
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
                }
            }
            else
            {
                Obavijest.Text = "Predugi ili prektratki uzorak";
            }
        }


        private void ProvjeriIspravnostUzorka(string uzorak)
        {
            if (noviPattern)
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

        }

        private void PohraniUzorakUBazu(string lozinka)
        {

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