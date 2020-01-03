using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class Statistika
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
    public class StatistikaController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/Statistika
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Statistika/5
        public Statistika Get(int id)
        {
            Statistika trenutnaStatistika = new Statistika();

            //Dohvacanje trenutnog broja rezervacija
            trenutnaStatistika.trenutniBrojRezervacija = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 3 && x.datum_do > DateTime.Now).Count();
            //Dohvacanje trenutnog broja posudenih publikacija
            trenutnaStatistika.trenutniBrojPosudba = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 2 && x.datum_do > DateTime.Now).Count();
            //Dohvacanje ukupnog broja rezerviracija
            trenutnaStatistika.ukupniBrojRezervacija = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 3).Count();
            //Dohvacanje ukupnog broja posudenih publikacija
            trenutnaStatistika.ukupniBrojPosudba = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 2).Count();
            //Dohvacanje ukupnog broja rezerviranih dana
            trenutnaStatistika.ukupniBrojRezerviranihDana = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 3).Sum(z => z.datum_do.Value.Subtract(z.datum).TotalDays);
            //Dohvacanje ukupnog broja posudenih dana
            trenutnaStatistika.ukupniBrojPosudenihDana = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 2).Sum(z => z.datum_do.Value.Subtract(z.datum).TotalDays);
            //Najraniji istek rezervacije
            int idRezerviraneKopije = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 3 && x.datum_do == db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 3).Min(v => v.datum_do)).Select(z => z.KopijaId).FirstOrDefault();
            var nazivRezerviraneKopije = (from Publikacija in db.Publikacije
                                          join Kopija in db.Kopija_Publikacije on Publikacija.id equals Kopija.PublikacijeId
                                          where Kopija.kopija_id == idRezerviraneKopije
                                          select Publikacija.naziv).First();

            trenutnaStatistika.najranijiIstekRezervacijeNaziv = nazivRezerviraneKopije;
            trenutnaStatistika.najranijiIstekRezervacijeDatum = db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 3).Min(v => v.datum_do).Value;
            //Najraniji istek posudbe
            int idPosudeneKopije = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 2 && x.datum_do == db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 2).Min(v => v.datum_do)).Select(z => z.KopijaId).FirstOrDefault();
            var nazivPosudeneKopije = (from Publikacija in db.Publikacije
                                          join Kopija in db.Kopija_Publikacije on Publikacija.id equals Kopija.PublikacijeId
                                          where Kopija.kopija_id == idPosudeneKopije
                                          select Publikacija.naziv).First();

            trenutnaStatistika.najranijiIstekPosudbeNaziv = nazivRezerviraneKopije;
            trenutnaStatistika.najranijiIstekPosudbeDatum = db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 2).Min(v => v.datum_do).Value;


            return trenutnaStatistika;
        }

        // POST: api/Statistika
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Statistika/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Statistika/5
        public void Delete(int id)
        {
        }
    }
}
