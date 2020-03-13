using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Testing.Domain.Concreate;
using Testing.Domain.Entities;
/// <summary>
/// The Student controller in the PersonalArea class displays personal customer data.
/// </summary>
namespace Testing.WebUI.Controllers
{
    [Authorize(Roles ="user")]
    public class StudentController : Controller
    {
        private DataContext context = new DataContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PersonalArea()
        {
            int studentId = GetStudentId();
            List<User> _users = context.Users.Where(x=>x.SessionId == studentId).ToList();
            List<User> users = new List<User>();
                    foreach (var item in _users) 
                    {
                        users.Add(context.Users.FirstOrDefault(x=>x.UserId == item.UserId));
                    }
            
            return View(users);
        }

        private int GetStudentId()
        {
            return Convert.ToInt32(Session["StudentId"]);
        }


    }
}