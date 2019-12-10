using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class FavoritiController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET api/Favoriti
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

        // GET api/Favoriti/5
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

        public HttpResponseMessage DohvatiFavorite()
        {
            var favorit = db.Je_Favorit.FirstOrDefault();

            if (favorit == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // POST api/Favoriti
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Favoriti/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Favoriti/5
        public void Delete(int id)
        {
        }
    }
}