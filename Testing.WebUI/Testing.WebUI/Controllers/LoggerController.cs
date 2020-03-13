using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;


/// <summary>
/// When working with the application on the manager’s side, 
/// it is important to catch possible errors and exceptions that may arise in a timely manner.
/// </summary>


namespace Testing.WebUI.Controllers
{
    public class LoggerController : Controller
    {
        private DataContext context = new DataContext();
        public ActionResult Logger()
        {
            var visitors = new List<Visitor>();
            using (DataContext context = new DataContext()) 
            {
                visitors = context.Visitors.ToList();
            }
            return View(visitors);
        }

        public ActionResult ExceptionFillter() 
        {
            var model = context.ExceptionDetails;   
            return View(model);
        }
    }
}