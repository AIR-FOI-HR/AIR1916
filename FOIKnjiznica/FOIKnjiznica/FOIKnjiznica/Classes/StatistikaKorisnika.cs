using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public class StatistikaKorisnika
    {
        public int trenutniBrojRezervacija { get; set; }

        public int trenutniBrojPosudba { get; set; }

        public int ukupniBrojRezervacija { get; set; }

        public int ukupniBrojPosudba { get; set; }

        public double ukupniBrojRezerviranihDana { get; set; }

        public double ukupniBrojPosudenihDana { get; set; }

        public string najranijiIstekRezervacijeNaziv { get; set; }
        public DateTime najranijiIstekRezervacijeDatum { get; set; }

        public string najranijiIstekPosudbeNaziv { get; set; }
        public DateTime najranijiIstekPosudbeDatum { get; set; }
    }
}
