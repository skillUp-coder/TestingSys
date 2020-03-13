using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;

namespace Testing.Domain.Infrastructure
{
    public class DbInitializer:DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.Roles.Add(new Role { RoleId = 1, RoleName = "admin" });
            context.Roles.Add(new Role { RoleId = 2, RoleName = "manager" });
            context.Roles.Add(new Role { RoleId = 3, RoleName = "user" });
            context.Roles.Add(new Role { RoleId = 4, RoleName = "ban" });


            context.Users.Add(new User 
                                    {
                                        UserId = 1,
                                        Email="admin@gmail.com",
                                        Password = "Admin_123",
                                        RoleId = 1, 
                                           
                                    });
            context.Users.Add(new User 
                                        { UserId = 2,
                                          Email="student@gmail.com",
                                          Password="Student_123",
                                          RoleId = 3,
                                           FirstName = "Artur",
                                           LastName = "Demianets"
                                        });
            context.Users.Add(new User
                                        {
                                            UserId = 3,
                                            Email = "manager@gmail.com",
                                            Password = "Manager_123",
                                            RoleId = 2,
                                            FirstName = "User",
                                            LastName = "Userovich"
                                        });

            List<Student> students = new List<Student>() 
                                    {
                                         new Student(){ UserId = 2 }
                                    };
            

            

            List<Question> questions = new List<Question>() 
            {
                new Question(){ Title="256*9:", Answer = "2304", ChoiceOne = "2305", ChoiceTwo = "2301", ChoiceThree = "2306", ChoiceFour = "96",AddedTime = DateTime.Now, CourseId = 1  },
                new Question(){ Title="78*5:", Answer = "390", ChoiceOne = "378", ChoiceTwo = "380", ChoiceThree = "389", ChoiceFour = "391",AddedTime = DateTime.Now, CourseId = 1  },
                new Question(){ Title="45+89", Answer = "134", ChoiceOne = "135", ChoiceTwo = "136", ChoiceThree = "133", ChoiceFour = "132",AddedTime = DateTime.Now, CourseId = 1  },
                new Question(){ Title="98*5", Answer = "490", ChoiceOne = "489", ChoiceTwo = "488", ChoiceThree = "495", ChoiceFour = "494",AddedTime = DateTime.Now, CourseId = 1  },

                new Question(){ Title="Сapital of Japan", Answer = "Tokio", ChoiceOne = "Kasugai", ChoiceTwo = "Ханда", ChoiceThree = "İtinomiya", ChoiceFour = "Okadzaki",AddedTime = DateTime.Now, CourseId = 2  },
                new Question(){ Title="Сapital of Serbia", Answer = "Belgrade", ChoiceOne = "Valevo", ChoiceTwo = "Kraljevo ", ChoiceThree = "Krusevac", ChoiceFour = "Leskovac",AddedTime = DateTime.Now, CourseId = 2  },
                new Question(){ Title="Сapital of Switzerland ", Answer = "Berne", ChoiceOne = "Altdorf", ChoiceTwo = "Biel", ChoiceThree = "Lausanne ", ChoiceFour = "Lucerne",AddedTime = DateTime.Now, CourseId = 2  },


            };
                    foreach (var item in questions) 
                    {
                        context.Questions.Add(item);
                    };
                    

            context.Courses.Add(new Course {  Counter = 4,  CourseName = "Math", Complexity = "Hard", ExamTime = 30, Category = "Math", CourseText = "Read each question carefully to make sure you understand the type of answer required.If you choose to use a calculator, be sure it is permitted, is working on test day, and has reliable batteries. Use your calculator wisely. Solve the problem. Locate your solution among the answer choices. Make sure you answer the question asked. Make sure your answer is reasonable. Check your work." });
            context.Courses.Add(new Course {  Counter = 3, CourseName = "Geography", Complexity = "Easy", ExamTime = 45, Category = "Geography", CourseText = "Take the multiple-choice challenge and learn about the continents, countries, oceans and vast bodies of water that are part of planet Earth." });
            
            
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
