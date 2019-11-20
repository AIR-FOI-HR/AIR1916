﻿using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class PublikacijeController : ApiController
    {
        FOIKnjiznicaEntities db = new FOIKnjiznicaEntities();

        // GET: api/Publikacije
        public IEnumerable<Object> Get()
        {
            var upit = from Publikacije in db.Publikacije select new { Publikacije.id, Publikacije.naziv, Publikacije.isbn, Publikacije.udk,
                Publikacije.signatura, Publikacije.jezik, Publikacije.stranice, Publikacije.sadrzaj, Publikacije.godina_izdanja,
                Publikacije.izdanje, Publikacije.slika_url};
            return upit.ToList<Object>();
        }

        // GET: api/Publikacije/5
        public string Get(int id)
        {
            return "value";
        }

        public HttpResponseMessage DohvatiPublikacije()
        {
            var publikacije = db.Publikacije.FirstOrDefault();

            if (publikacije == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // POST: api/Publikacije
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Publikacije/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Publikacije/5
        public void Delete(int id)
        {
        }
    }
}
