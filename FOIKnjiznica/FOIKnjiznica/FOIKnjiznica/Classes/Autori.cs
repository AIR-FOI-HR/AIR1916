using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public class Autori
    {
        public int id { get; set; }

        public string ime { get; set; }

        public string prezime { get; set; }

        public bool odabrano { get; set; } = false ;
    }
}
