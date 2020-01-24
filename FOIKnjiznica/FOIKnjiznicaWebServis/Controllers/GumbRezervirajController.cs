using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class GumbRezervirajController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();
        // GET: api/GumbRezerviraj
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GumbRezerviraj/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GumbRezerviraj
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GumbRezerviraj/5
        public void Put([FromBody] PovijestPublikacije novoStanjePublikacije)
        {
            //Dohvaćanje zadnjeg stanja kopije publikacije
            var upit = db.Stanje_Publikacije.Where(s => s.KopijaId == novoStanjePublikacije.kopijaId && s.datum_do == db.Stanje_Publikacije.Where(c => c.KopijaId == novoStanjePublikacije.kopijaId).Max(d => d.datum_do)).SingleOrDefault(); 

            if (upit.Vrsta_StatusaId == 1)
            {
                upit.datum_do = DateTime.Now;
                db.Stanje_Publikacije.Add(new Stanje_Publikacije() 
                { 
                    datum = novoStanjePublikacije.datum, 
                    datum_do = novoStanjePublikacije.datum_do, 
                    KopijaId = novoStanjePublikacije.kopijaId, 
                    Vrsta_StatusaId = novoStanjePublikacije.vrsta_statusaId,
                    ClanoviId = novoStanjePublikacije.clanoviId 
                });
                db.SaveChanges();             
            }

            DateTime dt1 = DateTime.Parse(upit.datum_do.ToString());
            DateTime dt2 = DateTime.Now;
            if (dt1 > dt2 && upit.Vrsta_StatusaId == 3)
            {
                upit.Vrsta_StatusaId = 1;
            }
        }

        // DELETE: api/GumbRezerviraj/5
        public void Delete(int id)
        {
        }
    }
}
