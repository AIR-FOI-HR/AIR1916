using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InterfaceModule;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PINModul
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PINUI : ContentPage
    {

        private List<string> pinNumber = new List<string>();
        private SHA256 sha256;
        private int brojac = 0;
        public string hashiraniPin;
        public Action<Type> zatvori;
        public Action<string> vrati;
        public string mode;
        int brojacUnosa = 0;

        public PINUI() { InitializeComponent();}
        public PINUI(string hashiraniPodatak, Action<Type> zatvaranjeUI)
        {
            InitializeComponent();
            sha256 = SHA256.Create();
            hashiraniPin = hashiraniPodatak;
            zatvori = zatvaranjeUI;
        }
        public PINUI(string hashiraniPodatak, Action<Type> zatvaranjeUI,Action<string> vratiUnesenuVrijednost)
        {
            InitializeComponent();
            sha256 = SHA256.Create();
            hashiraniPin = hashiraniPodatak;
            zatvori = zatvaranjeUI;
            vrati = vratiUnesenuVrijednost;
            mode = "Izmjena";

        }
        private void ProvjeriIspravnostUnosa()
        {
            int pin = SpojiBrojeve();

            if(mode == "Izmjena")
            {
                brojacUnosa++;
                if (brojacUnosa == 1)
                {
                    Naslov.Text = "";
                    hashiraniPin = HashirajPin(pin);
                    NeispravnaLozinka("Potvrdite unos");
                }
                else if(brojacUnosa == 2)
                {
                    if (hashiraniPin == HashirajPin(pin))
                    {
                        vrati(hashiraniPin);
                        zatvori(this.GetType());
                    }
                    else
                    {
                        brojacUnosa = 0;
                        Naslov.Text = "Neispravna lozinka!";
                        NeispravnaLozinka("Ponovno unesite PIN");  
                    }
                }
            }
            else
            {
                if (hashiraniPin == HashirajPin(pin))
                {
                    zatvori(this.GetType());
                }
                else
                {
                    Naslov.Text = "Neispravna lozinka!";
                    NeispravnaLozinka("Ponovno unesite PIN");
                }
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
                ProvjeriIspravnostUnosa();
                brojac = 0;
                GumbUnos1.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos2.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos3.BackgroundColor = Color.FromHex("#FFFFFF");
                GumbUnos4.BackgroundColor = Color.FromHex("#FFFFFF");
            }
        }

        private int SpojiBrojeve()
        {
            string stringPin = String.Join("", pinNumber.ToArray());
            int pin = Int32.Parse(stringPin);
            return pin;
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
        }
        private void NeispravnaLozinka(string porukaKorisniku)
        {
            Vibration.Vibrate();
            IspisKrivo.Text = porukaKorisniku;
            pinNumber.Clear();
            brojac = 0;
            GumbUnos1.BackgroundColor = Color.FromHex("#FFFFFF");
            GumbUnos2.BackgroundColor = Color.FromHex("#FFFFFF");
            GumbUnos3.BackgroundColor = Color.FromHex("#FFFFFF");
            GumbUnos4.BackgroundColor = Color.FromHex("#FFFFFF");
        }
        private string HashirajPin(int pin)
        {
            byte[] pinByte = Encoding.UTF8.GetBytes(pin.ToString());
            byte[] izracunatiPinByte = sha256.ComputeHash(pinByte);
            string pinHash = Convert.ToBase64String(izracunatiPinByte);
            return pinHash;
        }
    }
}