using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class PublikacijeController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/Publikacije
        public IEnumerable<Object> Get()
        {
            var upit = from Publikacije in db.Publikacije
                       join Izdavaci in db.Izdavaci on Publikacije.IzdavaciId equals Izdavaci.id
                       join Je_Autor in db.Je_Autor on Publikacije.id equals Je_Autor.PublikacijeId
                       join Autori in db.Autori on Je_Autor.AutoriId equals Autori.id
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
                           Autor = Autori.ime + " " + Autori.prezime
                       }; 
                       return upit.ToList<Object>();
        }

        // GET: api/Publikacije/5
        public List<Object> Get(int id)
        {
            var upit = from Publikacije in db.Publikacije
                       join Izdavaci in db.Izdavaci on Publikacije.IzdavaciId equals Izdavaci.id
                       join Je_Autor in db.Je_Autor on Publikacije.id equals Je_Autor.PublikacijeId
                       join Autori in db.Autori on Je_Autor.AutoriId equals Autori.id
                       join Kopija_Publikacije in db.Kopija_Publikacije on Publikacije.id equals Kopija_Publikacije.PublikacijeId
                       join Stanje_Publikacije in db.Stanje_Publikacije on Kopija_Publikacije.kopija_id equals Stanje_Publikacije.KopijaId
                       join Vrsta_Statusa in db.Vrsta_Statusa on Stanje_Publikacije.Vrsta_StatusaId equals Vrsta_Statusa.id
                       where id == Publikacije.id
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
                           Stanje = Stanje_Publikacije.datum_do,
                           Vrsta = Vrsta_Statusa.naziv_vrste
                       };
            return upit.ToList<Object>();
        }

        public HttpResponseMessage DohvatiPublikacije()
        {
            var publikacije = db.Publikacije.FirstOrDefault();

            if (publikacije == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // POST: api/Publikacije
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Publikacije/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Publikacije/5
        public void Delete(int id)
        {
        }
    }
}
