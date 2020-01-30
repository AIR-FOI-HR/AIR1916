using System;
using System.Collections.Generic;
using System.Text;
using InterfaceModule;
using Xamarin.Forms;

namespace PINModul
{
    public class PINPrijava : IPrijava
    {
        public bool StanjeZadnjePrijave { get; set; }
        public string UneseniPodatak { get; set; }

        public void PrijavaModulom(Action<Type,Action<Type>,string> otvoriUI, Action<Type> zatvoriUI,string hashiraniPodatak)
        {
            otvoriUI(typeof(PINUI),zatvoriUI,hashiraniPodatak);

            this.StanjeZadnjePrijave = true;
        }

        public void PrijavaModulom(Action<Type> zatvaranjeUI)
        {
            this.StanjeZadnjePrijave = false;
        }

        public void PromjenaPodataka(Action<Type, Action<Type>, string, Action<string>> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak)
        {
            otvaranjeUI(typeof(PINUI), zatvaranjeUI, HashiraniPodatak,VratiUneseniPodatak);

        }

        public void VratiUneseniPodatak(string proslijedeniPodatak)
        {
            this.UneseniPodatak = proslijedeniPodatak;
        }
    }
}
