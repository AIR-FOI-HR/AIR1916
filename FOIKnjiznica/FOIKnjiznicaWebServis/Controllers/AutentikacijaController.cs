using DotNetCasClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class AutentikacijaController : Controller
    {
        // 
        //GET: /Autentikacija/

        public string Index()
        {
            return HttpContext.User.Identity.Name;
        }
        

        //
        //GET: /Odjava/

        public void Odjava()
        {
            CasAuthentication.SingleSignOut();
        }
    }
}