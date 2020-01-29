using System;
using System.Collections.Generic;
using System.Text;
using PINModul;

namespace FOIKnjiznica.Classes
{
    public static class ImplementiraniModuli
    {
        static Dictionary<string, Type> popisModula = new Dictionary<string, Type>() 
        { 
            { "4", typeof(PINPrijava) } 
        }; 
    }
}
