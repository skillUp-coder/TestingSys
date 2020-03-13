using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;
using Testing.Domain.Infrastructure.Filters;
using Testing.WebUI.Models;
using Testing.WebUI.Models.ViewModels;
using Testing.WebUI.Services;

/// <summary>
/// Logon is implemented. 
/// Validation is also used in the login and registration classes.
/// Checking if the client exists, the password is entered correctly.
/// Forwarding by role.
/// </summary>

namespace Testing.WebUI.Controllers
{

    [Log]
    [Authorize]
    public class AccountController : Controller
    {
        private DataContext context = new DataContext();
        private LogAttribute LogAttr = new LogAttribute();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult _Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult _Login(LoginViewModel model) 
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DataContext context = new DataContext())
                {
                    user = context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                }
                if (user != null)
                {
                    if (user.RoleId == 1)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);

                        return RedirectToAction("AdminIndex", "Admin");


                    }
                    else if (user.RoleId == 3)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);

                        return RedirectToAction("Index", "Home");

                    }
                    else if (user.RoleId == 2)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("ManagerIndex", "Manager");

                    }
                    

                }
                else
                {
                    ModelState.AddModelError("", "A user with such a login and password is already there!");

                }
            }
            
            
            return View(model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult _Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult _Register(RegisterViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                User user = null;
                using (DataContext context = new DataContext())
                {
                    user = context.Users.FirstOrDefault(x => x.Email == model.Email);
                }
                if (user == null)
                {
                    using (DataContext context = new DataContext())
                    {
                        int studentID = context.Users.FirstOrDefault(x => x.UserId == x.RoleId).RoleId;
                        Session["StudentId"] = studentID;

                        context.Users.Add(new Domain.Entities.User { SessionId = Convert.ToInt32(Session["StudentId"]), Email = model.Email, Password = model.Password, RoleId = 3 , FirstName= model.FirstName, LastName = model.LastName});
                        
                        context.SaveChanges();
                        
                        user = context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();

                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже есть");
                }
            }
            
            return View(model);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("_Login", "Account");
        }



    }
}