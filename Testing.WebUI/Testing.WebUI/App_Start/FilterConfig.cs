using System.Web;

using System.Web.Mvc;
using Testing.Domain.Infrastructure.Filters;

namespace Testing.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionLoggerAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
