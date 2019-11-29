using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class AutoriController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/Autori
        public IEnumerable<Object> Get()
        {
            var upit = from Autori in db.Autori select new { Autori.id , Autori.ime, Autori.prezime};

            return upit.ToList<Object>();
        }

        // GET: api/Autori/5
        public string Get(int id)
        {
            return "value";
        }

        public HttpResponseMessage DohvatiAutore()
        {
            var autori = db.Autori.FirstOrDefault();

            if (autori == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // POST: api/Autori
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Autori/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Autori/5
        public void Delete(int id)
        {
        }
    }
}