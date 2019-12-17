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
                DisableBtn();
                System.Threading.Thread.Sleep(400);
                pinNumber.Clear();
                GumbUnos1.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos2.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos3.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos4.BackgroundColor = Color.FromHex("#FFFFFF");
                brojac = 0;
                EnableBtn();
            }
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
        private void BtnBack(object sender, EventArgs e)
        {
            
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

        private void DisableBtn()
        {
            Gumb1.IsEnabled = false;
            Gumb2.IsEnabled = false;
            Gumb3.IsEnabled = false;
            Gumb4.IsEnabled = false;
            Gumb5.IsEnabled = false;
            Gumb6.IsEnabled = false;
            Gumb7.IsEnabled = false;
            Gumb8.IsEnabled = false;
            Gumb9.IsEnabled = false;
            Gumb0.IsEnabled = false;

        }
        private void EnableBtn()
        {
            Gumb1.IsEnabled = true;
            Gumb2.IsEnabled = true;
            Gumb3.IsEnabled = true;
            Gumb4.IsEnabled = true;
            Gumb5.IsEnabled = true;
            Gumb6.IsEnabled = true;
            Gumb7.IsEnabled = true;
            Gumb8.IsEnabled = true;
            Gumb9.IsEnabled = true;
            Gumb0.IsEnabled = true;
        }
    }
}