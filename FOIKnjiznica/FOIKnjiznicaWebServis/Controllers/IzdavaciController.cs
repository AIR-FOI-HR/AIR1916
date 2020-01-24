using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class IzdavaciController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();

        // GET api/Izdavaci
        public IEnumerable<Object> Get()
        {
            var upit = from Izdavaci in db.Izdavaci select new { Izdavaci.id, Izdavaci.naziv, Izdavaci.adresa };

            return upit.ToList<Object>();
        }

        // GET api/Izdavaci/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Izdavaci
        public void Post([FromBody]string value)
        {
        }

        // PUT api/Izdavaci/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Izdavaci/5
        public void Delete(int id)
        {
        }
    }
}