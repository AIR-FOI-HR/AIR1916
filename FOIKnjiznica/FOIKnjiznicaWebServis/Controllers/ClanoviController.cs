﻿using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class ClanoviController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();

        // GET: api/Clanovi
        public IEnumerable<Object> Get()
        {
            //Dohvacanje svih članova
            var upit = from Clanovi in db.Clanovi
                       select new
                       {
                           Clanovi.hrEduPersonUniqueID,
                           Clanovi.mobitelID
                       };
            return upit.ToList<Object>();
        }

        // GET: api/Clanovi/5
        public object Get(string id)
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
            var clan = upit.First();
            return clan;
        }

        // POST: api/Clanovi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clanovi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clanovi/5
        public void Delete(int id)
        {
        }
    }
}
