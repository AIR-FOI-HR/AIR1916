using FOIKnjiznicaWebServis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class ClientAuth
    {
        public int ClanoviId { get; set; }
        public int Auth_ProtocolId { get; set; }
        public string podaci { get; set; }
    }
    public class DodajAuthProtocolController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();
        // GET: api/DodajAuthProtocol
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DodajAuthProtocol/5
        public List<Object> Get(int id)
        {
            var upit = (from s in db.Clanovi_Auth_Protocol
                        where s.ClanoviId == id
                        select new
                        {
                            s.ClanoviId,
                            s.Auth_ProtocolId,
                            s.podaci
                        });


            return upit.ToList<Object>();
        }

        // POST: api/DodajAuthProtocol
        public void Post([FromBody]ClientAuth clijentAuth)
        {
            using (var ctx = new foiknjiznicaEntities())
            {
                var upit = ctx.Clanovi_Auth_Protocol.Where(x => x.ClanoviId == clijentAuth.ClanoviId).FirstOrDefault();
                if (upit != null)
                {
                    ctx.Entry(upit).State = System.Data.Entity.EntityState.Deleted;

                }
                ctx.Clanovi_Auth_Protocol.Add(new Clanovi_Auth_Protocol
                {
                    ClanoviId = clijentAuth.ClanoviId,
                    Auth_ProtocolId = clijentAuth.Auth_ProtocolId,
                    podaci = clijentAuth.podaci,
                    //odabrano = clijentAuth.odabrano
                });

                ctx.SaveChanges();
            }
        }

        // PUT: api/DodajAuthProtocol/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DodajAuthProtocol/5
        public void Delete(int id)
        {
        }
    }
}
