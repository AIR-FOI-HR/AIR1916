using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class Statistika
    {
        public int trenutniBrojRezervacija { get; set; }

        public int trenutniBrojPosudba { get; set; }

        public int ukupniBrojRezervacija { get; set; }

        public int ukupniBrojPosudba { get; set; }
        
        public int ukupniBrojRezerviranihDana { get; set; }

        public int ukupniBrojPosudenihDana { get; set; }

        [DataMember(IsRequired = true)]
        public string najranijiIstekRezervacijeNaziv { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime najranijiIstekRezervacijeDatum { get; set; }

        [DataMember(IsRequired = true)]
        public string najranijiIstekPosudbeNaziv { get; set; }

        [DataMember(IsRequired = true)]
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
            var upitRezervacije = (from zapis in db.Stanje_Publikacije
                                   where zapis.ClanoviId == id && zapis.Vrsta_StatusaId == 3
                                   select new
                                   {
                                       zapis.id,
                                       zapis.datum,
                                       zapis.datum_do,
                                       zapis.KopijaId,
                                       zapis.Vrsta_StatusaId,
                                       zapis.ClanoviId
                                   }).Select(z => (z.datum_do < z.datum) ? DbFunctions.DiffDays(z.datum_do.Value, z.datum).Value : DbFunctions.DiffDays(z.datum, z.datum_do.Value).Value).DefaultIfEmpty(0).Sum();

            trenutnaStatistika.ukupniBrojRezerviranihDana = upitRezervacije;
            //Dohvacanje ukupnog broja posudenih dana
            var upitPosudbe = (from zapis in db.Stanje_Publikacije
                               where zapis.ClanoviId == id && zapis.Vrsta_StatusaId == 2
                               select new
                               {
                                   zapis.id,
                                   zapis.datum,
                                   zapis.datum_do,
                                   zapis.KopijaId,
                                   zapis.Vrsta_StatusaId,
                                   zapis.ClanoviId
                               }).Select(z => (z.datum_do < z.datum) ? DbFunctions.DiffDays(z.datum_do.Value, z.datum).Value : DbFunctions.DiffDays(z.datum, z.datum_do.Value).Value).DefaultIfEmpty(0).Sum();

            trenutnaStatistika.ukupniBrojPosudenihDana = upitPosudbe;

            //Najraniji istek rezervacije
            int idRezerviraneKopije = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 3 && x.datum_do.Value == db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 3 && z.datum_do.Value > DateTime.Now).Min(v => v.datum_do.Value)).Select(z => z.KopijaId).DefaultIfEmpty(0).First();
            if (idRezerviraneKopije != 0) 
            { 
                var nazivRezerviraneKopije = (from Publikacija in db.Publikacije
                                             join Kopija in db.Kopija_Publikacije on Publikacija.id equals Kopija.PublikacijeId
                                             where Kopija.kopija_id == idRezerviraneKopije
                                             select Publikacija.naziv).FirstOrDefault();
                trenutnaStatistika.najranijiIstekRezervacijeNaziv = nazivRezerviraneKopije;
                trenutnaStatistika.najranijiIstekRezervacijeDatum = db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 3 && z.datum_do.Value > DateTime.Now).Min(v => v.datum_do.Value);
            }
            else
            {
                trenutnaStatistika.najranijiIstekRezervacijeNaziv = null;
            }

            //Najraniji istek posudbe

            int idPosudeneKopije = db.Stanje_Publikacije.Where(x => x.ClanoviId == id && x.Vrsta_StatusaId == 2 && x.datum_do.Value == db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 2 && z.datum_do.Value > DateTime.Now).Min(v => v.datum_do.Value)).Select(z => z.KopijaId).DefaultIfEmpty(0).First();
            if (idPosudeneKopije != 0)
            {
                var nazivPosudeneKopije = (from Publikacija in db.Publikacije
                                           join Kopija in db.Kopija_Publikacije on Publikacija.id equals Kopija.PublikacijeId
                                           where Kopija.kopija_id == idPosudeneKopije
                                           select Publikacija.naziv).FirstOrDefault();

                trenutnaStatistika.najranijiIstekPosudbeNaziv = nazivPosudeneKopije;
                trenutnaStatistika.najranijiIstekPosudbeDatum = db.Stanje_Publikacije.Where(z => z.ClanoviId == id && z.Vrsta_StatusaId == 2 && z.datum_do.Value > DateTime.Now).Min(v => v.datum_do.Value);

            }
            else
            {
                trenutnaStatistika.najranijiIstekPosudbeNaziv = null;
            }
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
