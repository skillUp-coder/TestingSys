using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Abstract;
using Testing.Domain.Concreate;
using Testing.WebUI.Models.ViewModels;

namespace Testing.WebUI.Controllers
{
    /// <summary>
    /// The Menu controller displays a menu for filtering data.
    /// </summary>
    [Authorize(Roles ="user")]
    public class NavigationController : Controller
    {

        private DataContext context = new DataContext();

        [HttpGet]
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            NavigationViewModel model = new NavigationViewModel
            {
                categories = context.Courses.Select(x => x.Category).Distinct(),
                
            };
            
            return PartialView(model);
        }
        
    }
}