using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public class Publikacije
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string isbn { get; set; }
        public string udk { get; set; }
        public string signatura { get; set; }
        public string jezik { get; set; }
        public int stranice { get; set; }
        public string sadrzaj { get; set; }
        public int godina_izdanja { get; set; }
        public string izdanje { get; set; }
        public string slika_url { get; set; }
        public string Izdavac { get; set; }
        public string Autor { get; set; }
        public string Kopija { get; set; }
        public string Stanje { get; set; }
        public string Vrsta { get; set; }
        public int Kategorija { get; set; }

    }
}
