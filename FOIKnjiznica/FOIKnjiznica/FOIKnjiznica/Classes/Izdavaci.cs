using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public class Izdavaci
    {
        public int id { get; set; }

        public string naziv { get; set; }

        public string adresa { get; set; }

        public bool odabrano { get; set; } = false;
    }
}
