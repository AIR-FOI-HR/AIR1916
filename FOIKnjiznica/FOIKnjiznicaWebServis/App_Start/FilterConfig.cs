using System.Web;
using System.Web.Mvc;

namespace FOIKnjiznicaWebServis
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //force authentication - kako bi se zatražio login putem foi CAS
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
