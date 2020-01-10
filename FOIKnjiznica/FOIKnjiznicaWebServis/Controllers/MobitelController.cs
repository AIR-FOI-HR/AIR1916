using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class MobitelController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/Mobitel
        public IEnumerable<Object> Get()
        {
            //Dohvaćanje svih mobitela radi provjere mobitelID-a
            var upit = from Clan in db.Clanovi
                       select new
                       {
                           Clan.mobitelID
                       };
            return upit.ToList<Object>();
        }

        // GET: api/Mobitel/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mobitel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mobitel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mobitel/5
        public void Delete(int id)
        {
        }
    }
}
