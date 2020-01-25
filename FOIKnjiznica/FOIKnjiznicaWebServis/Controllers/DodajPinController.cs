using FOIKnjiznicaWebServis.Models;
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
        public bool odabrano { get; set; }
    }
    public class DodajPinController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();
        // GET: api/DodajPin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DodajPin/5
        public ClientAuth Get(int id)
        {
            ClientAuth novi = new ClientAuth();
            var upit = (from s in db.Clanovi_Auth_Protocol
                        where s.ClanoviId == id
                        select new
                        {
                            s.ClanoviId,
                            s.Auth_ProtocolId,
                            s.podaci,
                            s.odabrano
                        });

            return novi;
        }

        // POST: api/DodajPin
        public void Post([FromBody]ClientAuth clijentAuth)
        {

            using (var ctx = new foiknjiznicaEntities())
            {
                var upit = db.Clanovi_Auth_Protocol.Where(x => x.ClanoviId == clijentAuth.ClanoviId).SingleOrDefault();
                if(upit != null)
                {
                    upit.Auth_ProtocolId = clijentAuth.Auth_ProtocolId;
                    upit.podaci = clijentAuth.podaci;
                    upit.odabrano = clijentAuth.odabrano;
                    
                }
                else
                {
                    ctx.Clanovi_Auth_Protocol.Add(new Clanovi_Auth_Protocol()
                    {
                        ClanoviId = clijentAuth.ClanoviId,
                        Auth_ProtocolId = clijentAuth.Auth_ProtocolId,
                        podaci = clijentAuth.podaci,
                        odabrano = clijentAuth.odabrano
                    });
                }
                ctx.SaveChanges();
            }
        }

        // PUT: api/DodajPin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DodajPin/5
        public void Delete(int id)
        {
        }
    }
}
