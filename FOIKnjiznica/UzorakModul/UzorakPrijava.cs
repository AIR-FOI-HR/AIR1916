using System;
using System.Collections.Generic;
using System.Text;
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

        public void PrijavaModulom(Action<Type> zatvaranjeUI)
        {
            this.StanjeZadnjePrijave = false;
        }

        public void PromjenaPodataka(Action<Type, Action<Type>, string> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak)
        {
            throw new NotImplementedException();
        }

        public void PromjenaPodataka(Action<Type, Action<Type>, string, Action<string>> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak)
        {
            throw new NotImplementedException();
        }

        public void VratiUneseniPodatak(string proslijedeniPodatak)
        {
            throw new NotImplementedException();
        }
    }
}
