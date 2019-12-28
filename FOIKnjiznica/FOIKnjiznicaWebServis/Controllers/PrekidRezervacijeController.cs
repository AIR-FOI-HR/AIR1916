using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class PovijestPublikacije
    {
        public int id { get; set; }
        public int kopijaId { get; set; }
        public int vrsta_statusaId { get; set; }
        public int clanoviId { get; set; }

        public DateTime datum { get; set; }
        public DateTime datum_do { get; set; }
        public string nazivPublikacije { get; set; }
        public string nazivStatusa { get; set; }
    }
    public class PrekidRezervacijeController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/PrekidRezervacije
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PrekidRezervacije/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PrekidRezervacije
        public void Post([FromBody]PovijestPublikacije trenutnoStanjePublikacije)
        {
            var upit = db.Stanje_Publikacije.Where(x => x.id == trenutnoStanjePublikacije.id).SingleOrDefault();

            //Promjena datuma do kad je rezervacija vrijedila na datum prekida rezervacije
            upit.datum_do = DateTime.Now;

            //Kreiranje novog stanja publikacije (slobodno)
            Stanje_Publikacije novoStanje = new Stanje_Publikacije() 
            {
                datum = DateTime.Now, 
                KopijaId = trenutnoStanjePublikacije.kopijaId, 
                Vrsta_StatusaId = 1, 
                ClanoviId = trenutnoStanjePublikacije.clanoviId
            };

            db.Stanje_Publikacije.Add(novoStanje);
            db.SaveChanges();
        }

        // PUT: api/PrekidRezervacije/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PrekidRezervacije/5
        public void Delete(int id)
        {
        }
    }
}
