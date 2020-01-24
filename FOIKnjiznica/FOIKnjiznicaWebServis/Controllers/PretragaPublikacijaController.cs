using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class PretragaPublikacijaController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();
        // GET: api/PretragaPublikacija
        public IEnumerable<Object> Get()
        {

            return new string[] { "value1", "value2" };
        }

        public List<Object> Get(string id)
        {
            var upit = from Publikacije in db.Publikacije join Izdavaci in db.Izdavaci on Publikacije.IzdavaciId
                       equals Izdavaci.id join Je_Autor in db.Je_Autor on Publikacije.id equals Je_Autor.PublikacijeId
                       join Autori in db.Autori on Je_Autor.AutoriId equals Autori.id where
                       Publikacije.naziv.Contains(id) || Publikacije.signatura.Contains(id) ||
                       Izdavaci.naziv.Contains(id) || Autori.ime.Contains(id) || Autori.prezime.Contains(id)
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
            var pohrana = upit.Distinct();
            return pohrana.ToList<Object>();
        }


        // POST: api/PretragaPublikacija
        public void Post([FromBody]string value)
        {
        
    }


        // PUT: api/PretragaPublikacija/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PretragaPublikacija/5
        public void Delete(int id)
        {
        }
    }
}
