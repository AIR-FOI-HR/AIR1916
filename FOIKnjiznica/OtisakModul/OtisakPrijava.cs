using System;
using System.Collections.Generic;
using System.Text;
using InterfaceModule;
using Plugin.Fingerprint;

namespace OtisakModul
{
    public class OtisakPrijava : IPrijava
    {
        public bool StanjeZadnjePrijave { get; set; }
        public string UneseniPodatak { get; set; }

        public void PrijavaModulom(Action<Type, Action<Type>, string> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak)
        {
            PrijavaModulom(zatvaranjeUI);
        }

        public async void PrijavaModulom(Action<Type> zatvaranjeUI)
        {
            var prijava = await CrossFingerprint.Current.AuthenticateAsync("Prislonite prst na čitač otiska!");

            if (prijava.Authenticated)
            {
                this.StanjeZadnjePrijave = true;
                zatvaranjeUI(null);
            }
            else
            {
                this.StanjeZadnjePrijave = false;
            }
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
