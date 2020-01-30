using System;
using System.Collections.Generic;
using System.Text;
using PINModul;
using OtisakModul;
using UzorakModul;
using InterfaceModule;

namespace FOIKnjiznica.Classes
{
    public static class ImplementiraniModuli
    {
        public static Dictionary<string, IPrijava> popisModula = new Dictionary<string, IPrijava>() 
        { 
            { "4", new PINPrijava() }, 
            { "3", new OtisakPrijava() } ,
            { "2", new UzorakPrijava() } 
        }; 
    }
}
