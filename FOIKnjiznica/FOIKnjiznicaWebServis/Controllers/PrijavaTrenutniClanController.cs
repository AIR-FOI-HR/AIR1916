using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class PrijavaTrenutniClanController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();

        // GET: api/PrijavaTrenutniClan
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PrijavaTrenutniClan/5
        public List<Object> Get(string id)
        {      
                //Dohvacanje člana po id-u
                var upit = from Clan in db.Clanovi
                           where Clan.mobitelID == id
                           select new
                           {
                               Clan.id,
                               Clan.hrEduPersonUniqueID,
                               Clan.mobitelID
                           };
                var clan = upit;
                return clan.ToList<Object>();
        }

        // POST: api/PrijavaTrenutniClan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PrijavaTrenutniClan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PrijavaTrenutniClan/5
        public void Delete(int id)
        {
        }
    }
}
