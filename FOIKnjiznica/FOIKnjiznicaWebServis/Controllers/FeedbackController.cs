using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class FeedbackController : ApiController
    {
        FOIKnjiznicaEntities database = new FOIKnjiznicaEntities();
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("Get_Feedback")]

        public IHttpActionResult Get_Feedback()
        {
            Autori autori = null;
            try
            {
                autori = database.Autori.ToList().FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            if (autori == null)
            {
                return Ok("Nisu pronadeni Autori!");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(autori);
                return Json(json);
            }
        }
    }
}
