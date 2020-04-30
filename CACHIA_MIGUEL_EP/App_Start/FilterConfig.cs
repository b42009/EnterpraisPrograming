using System.Web;
using System.Web.Mvc;

namespace CACHIA_MIGUEL_EP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
