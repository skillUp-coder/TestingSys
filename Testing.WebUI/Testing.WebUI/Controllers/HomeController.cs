using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Abstract;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;
using Testing.WebUI.Models;
using Testing.WebUI.Models.ViewModels;

namespace Testing.WebUI.Controllers
{

    /// <summary>
    /// In the Home controller, a list is displayed for the course.
    /// Sorting by name, by complexity, by the number of questions.
    /// </summary>
    

    [Authorize(Roles = "user,admin")]
    public class HomeController : Controller
    {
        

        public int PageSize = 2;
        DataContext context;
        
                    public HomeController()
                    {
                        
                        context = new DataContext();
                    }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string category, int page = 1, string complexity = null,  int count =0)
        {
            ViewBag.complexityEasyParam = String.IsNullOrEmpty(complexity) ? "Easy" : "";
            ViewBag.complexityHardParam = String.IsNullOrEmpty(complexity) ? "Hard" : "";
            


            List<Course> _course = context.Courses.ToList();
            
            foreach (var itm in _course)
            {
                
                for (int i = 0; i <= itm.CourseId; i++)
                {
                    List<int> counterList = new List<int>();
                    var _counter = context.Questions.Where(x => x.CourseId == i).Count();

                    ViewBag._Counter = (count == 0)? _counter:0;
                    
                }
            }


            
            
            CourseListViewModel model = new CourseListViewModel
            {
                Courses = context.Courses.Where(x => category == null || x.Category == category).Where(x=>complexity == null||x.Complexity == complexity).Where(x=>count==0||x.Counter==count).OrderBy(x => x.CourseId).Skip((page - 1) * PageSize).Take(PageSize),
                pageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? context.Courses.Count() : context.Courses.Where(x => x.Category == category).Count(),

                },
                CurrentCategory = category,
                
            };
            

            return View(model);
        }

        private List<Exam> GetLastThreeExamByStudentId()
        {
            int studentId = GetStudentId();
            return context.Exams.Where(x => x.StudentId == studentId).OrderByDescending(x => x.Date).Take(3).OrderBy(x => x.Date).ToList();
        }
        private int GetStudentId()
        {
            return Convert.ToInt32(Session["StudentId"]);
        }


    }
}