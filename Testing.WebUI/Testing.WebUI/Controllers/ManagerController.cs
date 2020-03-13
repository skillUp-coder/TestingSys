using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Concreate;

/// <summary>
/// In the manager controller, a list of clients with the (user) role is displayed.
/// Also the ability to log exceptions.
/// To catch errors and exceptions that may arise in a timely manner.
/// </summary>

namespace Testing.WebUI.Controllers
{

    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private DataContext context;
        public ManagerController()
        {
            context = new DataContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ManagerIndex()
        {
            var managermodel = context.Users.Where(x => x.RoleId >= 3).ToList();
            return View(managermodel);
        }
    }
}