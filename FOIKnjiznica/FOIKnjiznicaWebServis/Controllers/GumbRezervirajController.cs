using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class GumbRezervirajController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();
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
        public void Put(int id)
        {
            var upit = db.Stanje_Publikacije.Where(s => s.KopijaId == id).SingleOrDefault();

            if (upit.Vrsta_StatusaId == 1)
            {
                upit.Vrsta_StatusaId = 3;
                upit.datum = DateTime.Now;
                upit.datum_do = DateTime.Now.AddDays(5);
                db.SaveChanges();
            }
            DateTime dt1 = DateTime.Parse(upit.datum_do.ToString());
            DateTime dt2 = DateTime.Now;
            if (dt1 > dt2)
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
