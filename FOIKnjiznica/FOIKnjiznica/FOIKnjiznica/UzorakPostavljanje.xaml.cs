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
        }
    }
}