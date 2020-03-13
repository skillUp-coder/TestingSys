using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;


/// <summary>
/// This uses management for the administrator role.
/// The AdminListClients class displays a list of clients with the (user) role.
/// The LockOutUsers class locks the client.
/// In the UnLock class, you can unlock the client.
/// </summary>
namespace Testing.WebUI.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private DataContext context;
                                    public AdminController()
                                    {
                                        context = new DataContext();
                                    }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminIndex() 
        {
            return View();
        }

                            [HttpGet]
                            [AllowAnonymous]
                            public ActionResult AdminListClients() 
                            {
                                List<User> _users = context.Users.Where(x=>x.RoleId >= 3).ToList();
                                List<User> users = new List<User>();
                                foreach (var item in _users) 
                                {
                                    users.Add(context.Users.FirstOrDefault(x=>x.UserId == item.UserId));
                                }
                                return View(users);
                            }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LockOutUsers(int ? id) 
        {
            if (id == null) 
            {
                return HttpNotFound();
            }
            User user = context.Users.Find(id);
            if (user !=null) 
            {
                context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return View(user);
            }
            return HttpNotFound();
        }
                            [HttpPost]
                            [AllowAnonymous]
                            public ActionResult LockOutUsers(FormCollection form)
                            {

                                string FirstName = form["FirstName"];
                                string LastName = form["LastName"];
                                string Email = form["Email"];
                                string Password = form["Password"];
                                string RoleId = form["RoleId"];

                                List<User> userList = new List<User>()
                                {
                                                    new User()
                                                    {
                                                        FirstName = FirstName, LastName = LastName, Email = Email, Password = Password, RoleId = 4
                                                    }
                                };

                                context.Users.AddRange(userList);
                                context.SaveChanges();
                                return RedirectToAction("AdminListClients");
                            }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult UnLock(int? id) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return View(user);
            }
            return HttpNotFound();
        }

                        [HttpPost]
                        [AllowAnonymous]
                        public ActionResult UnLock(FormCollection form) 
                        {
                            string FirstName = form["FirstName"];
                            string LastName = form["LastName"];
                            string Email = form["Email"];
                            string Password = form["Password"];
                            List<User> userList = new List<User>()
                            {
                                        new User()
                                        {
                                            FirstName = FirstName, LastName = LastName, Email = Email, Password = Password, RoleId = 3
                                        }
                            };

                            context.Users.AddRange(userList);
                            context.SaveChanges();
                            return RedirectToAction("AdminListClients");
                        }
    }
}