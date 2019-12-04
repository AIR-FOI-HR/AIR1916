using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public static class Filtar
    {
        public static List<Autori> filtarAutori { get; set; } = new List<Autori>();
        public static List<Izdavaci> filtarIzdavaci { get; set; } = new List<Izdavaci>();
        public static List<Kategorije> filtarKategorije { get; set; } = new List<Kategorije>();
        public static List<Slova> filtarSlova { get; set; } = new List<Slova>();

        public static List<Publikacije> FiltrirajPublikacije(List<Publikacije> trenutnaListaPublikacija)
        {
            List<Publikacije> filtriranaListaPublikacija = new List<Publikacije>();
            List<Publikacije> privremenaFiltriranaListaPublikacija = new List<Publikacije>();
            bool prolaziFiltar = false;

            //Filtriranje po id autora ako filtar autora sadrži barem jedan element, ako ne filtriranaLista sadrži sve publikacije

            if (filtarAutori.Count > 0)
            {
                    foreach (Publikacije trenutnaPublikacija in trenutnaListaPublikacija)
                    {
                        prolaziFiltar = filtarAutori.Any(item => item.id == trenutnaPublikacija.id);

                        if(prolaziFiltar == true)
                        {
                            filtriranaListaPublikacija.Add(trenutnaPublikacija);
                            prolaziFiltar = false;
                        }
                    }
            }
            else
            {
                filtriranaListaPublikacija = trenutnaListaPublikacija;
            }

            //Filtriranje po id izdavaca ako filtar izdavaca sadrži barem jedan element, ako ne filtriranaLista sadrži sve publikacije
            if (filtarIzdavaci.Count > 0)
            {
                foreach (Publikacije trenutnaPublikacija in filtriranaListaPublikacija)
                {
                    prolaziFiltar = filtarIzdavaci.Any(item => item.id == trenutnaPublikacija.id);

                    if (prolaziFiltar == true)
                    {
                        privremenaFiltriranaListaPublikacija.Add(trenutnaPublikacija);
                        prolaziFiltar = false;
                    }
                }

                trenutnaListaPublikacija = privremenaFiltriranaListaPublikacija;
                privremenaFiltriranaListaPublikacija.Clear();
            }

            //Filtriranje po id kategorije ako filtar kategorija sadrži barem jedan element, ako ne filtriranaLista sadrži sve publikacije
            if (filtarKategorije.Count > 0)
            {
                foreach (Publikacije trenutnaPublikacija in filtriranaListaPublikacija)
                {
                    prolaziFiltar = filtarKategorije.Any(item => item.id == trenutnaPublikacija.id);

                    if (prolaziFiltar == true)
                    {
                        privremenaFiltriranaListaPublikacija.Add(trenutnaPublikacija);
                        prolaziFiltar = false;
                    }
                }

                trenutnaListaPublikacija = privremenaFiltriranaListaPublikacija;
                privremenaFiltriranaListaPublikacija.Clear();
            }

            //Filtriranje po početnom slovu ako filtar slova sadrži barem jedan element, ako ne filtriranaLista sadrži sve publikacije
            if (filtarSlova.Count > 0)
            {
                foreach (Publikacije trenutnaPublikacija in filtriranaListaPublikacija)
                {
                    prolaziFiltar = filtarSlova.Any(item => item.slovo == trenutnaPublikacija.naziv[0]);

                    if (prolaziFiltar == true)
                    {
                        privremenaFiltriranaListaPublikacija.Add(trenutnaPublikacija);
                        prolaziFiltar = false;
                    }
                }

                trenutnaListaPublikacija = privremenaFiltriranaListaPublikacija;
                privremenaFiltriranaListaPublikacija.Clear();
            }

            return filtriranaListaPublikacija;
        }

    }
}
