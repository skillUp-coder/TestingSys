using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;
using Testing.WebUI.Models.ViewModels;

namespace Testing.WebUI.Controllers
{
    /// <summary>
    ///In the Statistic controller in the StatisticList class
    ///displays the data of the test passed, as well as the graph of courses taken and correct answers.
    /// </summary>
    /// 
    [Authorize(Roles ="user")]
    public class StatisticController : Controller
    {
        private DataContext context = new DataContext();

        public ActionResult StatisticList(int? id)
        {
            List<DateTime> examTimes = GetExamTimes();
            List<SelectListItem> examTimesSelectList = new List<SelectListItem>();
            foreach (var item in examTimes) 
            {
                examTimesSelectList.Add(new SelectListItem() { Text = item.ToString(),Value = item.ToString() });

            }

            List<Question> questions = context.Questions.ToList();
            List<Course> courses = new List<Course>();


            List<Exam> exams = GetLastThreeExamByStudentId();
            foreach (Exam item in exams)
            {
                var _truecounter = item.TrueCounter;
                ViewBag._TrueCounter = _truecounter;

                List<Course> _course = context.Courses.ToList();
                foreach (var itm in _course) 
                {
                    var _counter = context.Questions.Where(x => x.CourseId == id).Count();
                    ViewBag._Count = _counter;
                    var _res = 100 / _counter * _truecounter;
                    ViewBag._Res = _res;
                    
                }
                
            }
            
            
            
            ViewBag.ExamTimes = examTimesSelectList;
            

            return View();
        }
        public ActionResult LastFreeExam() 
        {
            List<StatisticViewModel> result = new List<StatisticViewModel>();
            List<Exam> exams = GetLastThreeExamByStudentId();
            foreach (Exam item in exams) 
            {
                result.Add(new StatisticViewModel() { Point = item.TrueCounter, Date = item.Date.ToString().Split(' ')[0].ToString() });
            }
            
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryExam(string date)
        {
            int studentId = GetStudentId();
            DateTime dateTime = Convert.ToDateTime(date);
            List<Exam> exams = context.Exams.Where(x => x.Date.Day == dateTime.Day && x.Date.Month == dateTime.Month && x.Date.Year == dateTime.Year).ToList();
            List<CategoryStatisticViewModel> result = new List<CategoryStatisticViewModel>();
            CategoryStatisticViewModel temp;
            int point;
            string name;
            foreach (var item in exams)
            {

                List<Exam> tempExamCategory = context.Exams.Where(x => x.ExamId == item.ExamId).ToList();
                foreach (var value in tempExamCategory)
                {
                    point = value.TrueCounter;
                    name = context.Courses.FirstOrDefault(x => x.CourseId == value.ExamId).CourseName;
                    temp = new CategoryStatisticViewModel() { Point = point, Name = name };
                    result.Add(temp);

                }
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<Exam> GetLastThreeExamByStudentId() 
        {
            int studentId = GetStudentId();
            return context.Exams.Where(x=>x.StudentId == studentId).OrderByDescending(x=>x.Date).Take(3).OrderBy(x=>x.Date).ToList();
        }
        private int GetStudentId() 
        {
            return Convert.ToInt32(Session["StudentId"]);
        }
        private List<DateTime> GetExamTimes() 
        {
            int studentId = GetStudentId();
            List<Exam> exams = context.Exams.Where(x=>x.StudentId == studentId).ToList();
            List<DateTime> examTimes = new List<DateTime>();
            foreach (var item in exams) 
            {
                examTimes.Add(item.Date);
            }
            return examTimes;
        }
    }
}