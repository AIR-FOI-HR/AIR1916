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
    public partial class PinLogin : ContentPage
    {
        private List<string> pinNumber = new List<string>();
        private int brojac = 0;
        public PinLogin()
        {
            InitializeComponent();
        }

        private void DodajBroj(string broj)
        {

        }

        private void IzbrisiPrethodnuBrojku()
        {
            if (brojac > 0)
            {
                pinNumber.RemoveAt(pinNumber.Count - 1);
                if (brojac == 1)
                {
                    GumbUnos1.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                else if (brojac == 2)
                {
                    GumbUnos2.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                else if (brojac == 3)
                {
                    GumbUnos3.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                brojac--;
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
    }
}