using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class Je_Favorit_API
    {
        public int PublikacijeId { get; set; }
        public int ClanoviId { get; set; }
        public string pomocno { get; set; }

    }
    public class TestFavoritiController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/TestFavoriti
        public IEnumerable<Object> Get()
        {
            var upit = from Je_Favorit in db.Je_Favorit
                       select new
                       {
                           idFavorita = Je_Favorit.PublikacijeId,
                           idClana = Je_Favorit.ClanoviId
                       };

            return upit.ToList<Object>();
        }

        // GET: api/TestFavoriti/5
        public List<Object> Get(int id)
        {
            var upit = from Publikacije in db.Publikacije
                       join Izdavaci in db.Izdavaci on Publikacije.IzdavaciId equals Izdavaci.id
                       join Je_Autor in db.Je_Autor on Publikacije.id equals Je_Autor.PublikacijeId
                       join Autori in db.Autori on Je_Autor.AutoriId equals Autori.id
                       join Kategorije in db.Kategorije on Publikacije.KategorijeId equals Kategorije.id
                       join Je_Favorit in db.Je_Favorit on Publikacije.id equals Je_Favorit.PublikacijeId
                       where id == Je_Favorit.ClanoviId
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
                           Kategorija = Kategorije.id
                       };
            return upit.ToList<Object>();
        }

        // POST: api/TestFavoriti
        public IHttpActionResult Post([FromBody]Je_Favorit_API content)
        {

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new KnjiznicaEntities())
            {
                ctx.Je_Favorit.Add(new Je_Favorit()
                {
                    ClanoviId = content.ClanoviId,
                    PublikacijeId = content.PublikacijeId,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT: api/TestFavoriti/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TestFavoriti/5
        public void Delete(int id)
        {
        }
    }
}
