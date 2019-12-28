using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class PovijestPosudbiController : ApiController
    {
        KnjiznicaEntities db = new KnjiznicaEntities();

        // GET: api/PovijestPosudbi
        public IEnumerable<Object> Get()
        {
            var upit = from Stanje in db.Stanje_Publikacije
                       join Kopija in db.Kopija_Publikacije on Stanje.KopijaId equals Kopija.kopija_id
                       join Publikacija in db.Publikacije on Kopija.PublikacijeId equals Publikacija.id
                       join VrstaStatusa in db.Vrsta_Statusa on Stanje.Vrsta_StatusaId equals VrstaStatusa.id
                       join Clan in db.Clanovi on Stanje.ClanoviId equals Clan.id
                       select new
                       {
                           Stanje.id,
                           Stanje.KopijaId,
                           Stanje.Vrsta_StatusaId,
                           Stanje.ClanoviId,
                           Stanje.datum,
                           Stanje.datum_do,
                           nazivPublikacije = Publikacija.naziv,
                           nazivStatusa = VrstaStatusa.naziv_vrste
                       };
            return upit.ToList<Object>();
        }

        // GET: api/PovijestPosudbi/5
        public IEnumerable<Object> Get(int id)
        {
            var upit = from Stanje in db.Stanje_Publikacije
                       join Kopija in db.Kopija_Publikacije on Stanje.KopijaId equals Kopija.kopija_id
                       join Publikacija in db.Publikacije on Kopija.PublikacijeId equals Publikacija.id
                       join VrstaStatusa in db.Vrsta_Statusa on Stanje.Vrsta_StatusaId equals VrstaStatusa.id
                       join Clan in db.Clanovi on Stanje.ClanoviId equals Clan.id
                       where Clan.id == id
                       select new
                       {
                           Stanje.id,
                           Stanje.KopijaId,
                           Stanje.Vrsta_StatusaId,
                           Stanje.ClanoviId,
                           Stanje.datum,
                           Stanje.datum_do,
                           nazivPublikacije = Publikacija.naziv,
                           nazivStatusa = VrstaStatusa.naziv_vrste
                       };
            return upit.ToList<Object>();
        }

        // POST: api/PovijestPosudbi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PovijestPosudbi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PovijestPosudbi/5
        public void Delete(int id)
        {
        }
    }
}
