using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class KategorijeController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET api/Kategorije
        public IEnumerable<Object> Get()
        {
            var upit = from Kategorije in db.Kategorije select new { Kategorije.id, Kategorije.naziv_kategorije };

            return upit.ToList<Object>();
        }

        // GET api/Kategorije/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Kategorije
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Kategorije/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Kategorije/5
        public void Delete(int id)
        {
        }
    }
}