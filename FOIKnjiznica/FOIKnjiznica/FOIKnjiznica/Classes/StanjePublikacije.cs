using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public class StanjePublikacije
    {
        public int id { get; set; }
        public int kopijaId { get; set; }
        public int vrsta_statusaId { get; set; }
        public int clanoviId { get; set; }

        public DateTime datum { get; set; }
        public DateTime datum_do { get; set; }
        public string nazivPublikacije { get; set; }
        public string nazivStatusa { get; set; }

        public Xamarin.Forms.Color bojaPozadine { get; set; }
    }
}
