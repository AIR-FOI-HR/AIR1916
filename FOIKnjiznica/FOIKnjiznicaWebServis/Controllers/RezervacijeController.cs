using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class RezervacijeController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();
        // GET: api/Rezervacije
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Rezervacije/5
        public List<Object> Get(int id)
        {
            var upit = from Publikacije in db.Publikacije
                       join Izdavaci in db.Izdavaci on Publikacije.IzdavaciId equals Izdavaci.id
                       join Je_Autor in db.Je_Autor on Publikacije.id equals Je_Autor.PublikacijeId
                       join Autori in db.Autori on Je_Autor.AutoriId equals Autori.id
                       join Kopija_Publikacije in db.Kopija_Publikacije on Publikacije.id equals Kopija_Publikacije.PublikacijeId
                       join Stanje_Publikacije in db.Stanje_Publikacije on Kopija_Publikacije.kopija_id equals Stanje_Publikacije.KopijaId
                       join Vrsta_Statusa in db.Vrsta_Statusa on Stanje_Publikacije.Vrsta_StatusaId equals Vrsta_Statusa.id
                       where id == Kopija_Publikacije.kopija_id
                       select new
                       {
                           Publikacije.id,
                           Publikacije.naziv,
                           Publikacije.isbn,
                           Publikacije.udk,
                           Publikacije.signatura,
                           Publikacije.jezik,
                           Publikacije.stranice,
                           Publikacije.sadrzaj,
                           Publikacije.godina_izdanja,
                           Publikacije.izdanje,
                           Publikacije.slika_url,
                           Izdavac = Izdavaci.naziv,
                           Autor = Autori.ime + " " + Autori.prezime,
                           Kopija = Kopija_Publikacije.kopija_id,
                           GodinaOd = Stanje_Publikacije.datum,
                           GodinaDo = Stanje_Publikacije.datum_do,
                           Vrsta = Vrsta_Statusa.naziv_vrste
                       };
            return upit.ToList<Object>();
        }

        // POST: api/Rezervacije
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Rezervacije/5
        public void Put()
        {
        }

        // DELETE: api/Rezervacije/5
        public void Delete(int id)
        {
        }
    }
}
