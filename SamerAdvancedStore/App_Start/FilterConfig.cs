using System.Web;
using System.Web.Mvc;
using SamerAdvancedStore.Filters;

namespace SamerAdvancedStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
       
        }
    }
}
