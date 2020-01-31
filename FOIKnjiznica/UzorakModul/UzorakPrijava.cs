using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InterfaceModule;

namespace UzorakModul
{
    public class UzorakPrijava : IPrijava
    {
        public bool StanjeZadnjePrijave { get; set; }
        public string UneseniPodatak { get; set; }

        public void PrijavaModulom(Action<Type, Action<Type>, string> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak)
        {
            otvaranjeUI(typeof(UzorakUI), zatvaranjeUI, HashiraniPodatak);

            this.StanjeZadnjePrijave = true;
        }
        public void PromjenaPodataka(Action<Type, Action<Type>, string, Action<string>> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak)
        {
            otvaranjeUI(typeof(UzorakUI), zatvaranjeUI, HashiraniPodatak, VratiUneseniPodatak);
        }

        public void VratiUneseniPodatak(string proslijedeniPodatak)
        {
            this.UneseniPodatak = proslijedeniPodatak;
        }
    }
}
