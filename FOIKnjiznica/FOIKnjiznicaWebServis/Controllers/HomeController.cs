using DotNetCasClient;
using DotNetCasClient.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = HttpContext.User.Identity.Name.ToString();
            return View();
        }
    }
}
