using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public class Kategorije
    {
        public int id { get; set; }
        public string naziv_kategorije { get; set; }

        public bool odabrano { get; set; } = false;
    }
}
