using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModule
{
    public interface IPrijava
    {
        bool StanjeZadnjePrijave { get; set; }
        string UneseniPodatak { get; set; }
        void PrijavaModulom(Action<Type,Action<Type>,string> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak);       
        void PromjenaPodataka(Action<Type,Action<Type>,string> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak);       
    }
}
