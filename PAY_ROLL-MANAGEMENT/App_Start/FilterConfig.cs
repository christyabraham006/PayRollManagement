using System.Web;
using System.Web.Mvc;

namespace PAY_ROLL_MANAGEMENT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
