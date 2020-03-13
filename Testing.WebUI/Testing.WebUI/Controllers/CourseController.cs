using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Abstract;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;
using Testing.Domain.Infrastructure.Filters;

/// <summary>
///In this controller, a course with tests is displayed.
///In the CourseList class, the correct course is added to the database, wrong answers, time, studentId.
///In the create class, the administrator creates a course and tests, time.
///In the DeleteCource class, the administrator can delete the course.
/// </summary>

namespace Testing.WebUI.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class CourseController : Controller
    {
        private static Dictionary<int, int> _questionIdList ;
        

       
        DataContext context = new DataContext();
        public CourseController()
        {
            
            
        }
        [ExceptionLogger]
        [HttpGet]
        [AllowAnonymous]     
        public ActionResult CourseList(int? id)
        {
            if (id == null) 
            {
                return HttpNotFound();
            }
            
            var model = context.Questions.Where(x=>x.CourseId == id).ToList();
            var minutsCounter = context.Courses.Where(x=>x.CourseId == id).Select(x => x.ExamTime).FirstOrDefault();

            var courses = context.Courses.Where(x=>x.CourseId==id).FirstOrDefault();
            var courseName = courses.CourseName;
            ViewBag._CourseName = courseName;

            MatchQuestionsToCategoryId(model);
            ViewBag.MinutsCounter = minutsCounter;

            
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public ActionResult CourseList(FormCollection form,int? id)
        {
            
                int trueCounter = 0;
                int falseCounter = 0;
                
                
                Exam exam = new Exam { StudentId = Convert.ToInt32(Session["StudentId"]), Date = DateTime.Now};
                
                List<ExamCategory> examCategories = new List<ExamCategory>();
                foreach (var item in _questionIdList)
                {
                    if (form[item.Key.ToString()] == null)
                        continue;
                    string result = form[item.Key.ToString()];
                    string dbAnswer = GetRightAnswer(item.Key);

                    bool isNewCategory = true;
                    foreach (ExamCategory examCategory in examCategories.Where(x => x.CategoryId == item.Value))
                    {
                        isNewCategory = false;
                        if (result == dbAnswer)
                        {
                            examCategory.TrueCounter += 1;
                            trueCounter += 1;
                        }
                        else
                        {
                            examCategory.FalseCounter += 1;
                            falseCounter += 1;
                        }
                    }

                    if (!isNewCategory) continue;

                    ExamCategory newExamCategory = new ExamCategory { CategoryId = item.Value, TrueCounter = 0, FalseCounter = 0 };

                    examCategories.Add(newExamCategory);
                    if (result == dbAnswer)
                    {
                        newExamCategory.TrueCounter += 1;
                        trueCounter += 1;
                    }
                    else
                    {
                        newExamCategory.FalseCounter += 1;
                        falseCounter += 1;
                    }
                }
                exam.TrueCounter = trueCounter;
                exam.FalseCounter = falseCounter;
                exam.Point = trueCounter * 2;

                context.Exams.Add(exam);
                //context.Users.Add(user);
            
                context.SaveChanges();

            
            
            return RedirectToAction($"StatisticList/{id}", "Statistic");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create() 
        {
            List<Course> courses = GetCourses();
            ViewBag.Categories = new SelectList(courses, "CourseId", "Category");
            ViewBag.Complaxities = new SelectList(courses, "Complexity", "Complexity");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form) 
        {
            string Title = form["title"];
            string Answer = form["answer"];
            string CategoryId = form["Categories"];
            string ChoiceOne = form["choiceOne"];
            string ChoiceTwo = form["choiceTwo"];
            string ChoiceThree = form["choiceThree"];
            string ChoiceFour = form["choiceFour"];

            int c_Id = Convert.ToInt32(CategoryId);
            Question question = new Question()
            {
                Title = Title,
                Answer = Answer,
                CourseId = c_Id,
                AddedTime = DateTime.Now,
                ChoiceOne = ChoiceOne,
                ChoiceTwo = ChoiceTwo,
                ChoiceThree = ChoiceThree,
                ChoiceFour = ChoiceFour,
                
            };

            
            Course course = new Course()
            {
                
                Counter = context.Questions.Where(x => x.CourseId == c_Id).Count()
            };

            
            context.Questions.Add(question);
            context.SaveChanges();


            
            return RedirectToAction("AdminIndex", "Admin");


        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult DeleteCource() 
        {
            var model = context.Courses.ToList();
            return View(model);
        }

        

        public ActionResult DeleteDataCourse(int ? id ) 
        {
            var datacourse = context.Courses.Find(id);
            if (id != null)
            {

            }
            context.Courses.Remove(datacourse);
            context.SaveChanges();
            return RedirectToAction("AdminIndex", "Admin");

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(FormCollection form) 
        {
            string CategoryName = form["name"];
            string Time = form["time"];
            string Complexity = form["Complaxities"];

            
            Course course = new Course()
            {
                Category = CategoryName,
                Complexity = Complexity,
                CourseName = CategoryName,
                ExamTime = Convert.ToInt32(Time)
            };
            context.Courses.Add(course);
            context.SaveChanges();
            return RedirectToAction("Create","Course");
        }
        private List<Course> GetCourses() 
        {
            return context.Courses.ToList();
        }


        private string GetRightAnswer(int questionId)
        {
            return context.Questions.FirstOrDefault(x => x.QuestionId == questionId).Answer;
        }

        private void MatchQuestionsToCategoryId(List<Question> questions)  // Parametre olarak gelen sorulari key => soru, value => sorunun kategori id'si olacak sekilde _questionsIdList adli listeye ekler.
        {
            _questionIdList = new Dictionary<int, int>();

            
                foreach (var item in questions)
                {
                    int categoryId = context.Questions.FirstOrDefault(x => x.QuestionId == item.QuestionId).CourseId;
                    _questionIdList.Add(item.QuestionId, categoryId);
                }
            
        }


    }
}
