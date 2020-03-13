using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;

namespace Testing.Domain.Infrastructure.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail()
            {
                ExceotionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };

            using (DataContext context = new DataContext()) 
            {
                context.ExceptionDetails.Add(exceptionDetail);
                context.SaveChanges();
            }
            filterContext.ExceptionHandled = true;
        }
    }
}
