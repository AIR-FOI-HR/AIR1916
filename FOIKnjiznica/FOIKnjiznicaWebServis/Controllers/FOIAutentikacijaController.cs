using FOIKnjiznicaWebServis.Models;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class Clan_API
    {
        public string IdUsera { get; set; }
        public string IdMobitela { get; set; }
    }
    public class FOIAutentikacijaController : ApiController
    {
        // GET: api/FOIAutentikacija
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FOIAutentikacija/5
        public string Get(int id)
        {
            return "1";
        }

        // POST: api/FOIAutentikacija
        public IHttpActionResult Post([FromBody]Clan_API clanPost)
        {
            if (!ModelState.IsValid) { return BadRequest("Invalid data."); }

            string mail = GenerirajMail(clanPost.IdUsera);

            string mobitelIDLocal = clanPost.IdMobitela;
            using(var context = new foiknjiznicaEntities())
            {
                var clan = context.Clanovi.Where(c => c.hrEduPersonUniqueID == mail).FirstOrDefault();

                if(clan != null)
                {
                    clan.mobitelID = mobitelIDLocal;
                }

                else
                {
                    context.Clanovi.Add(new Clanovi
                    {
                        hrEduPersonUniqueID = mail,
                        mobitelID = mobitelIDLocal
                    });
                }
                context.SaveChanges();
            }

            return Ok();
            //return clan_API;

        }

        private string GenerirajMail(string uid)
        {
            return uid + "@foi.hr";
        }

        // PUT: api/FOIAutentikacija/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FOIAutentikacija/5
        public void Delete(int id)
        {
        }
    }
}
