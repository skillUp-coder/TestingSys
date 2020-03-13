using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Domain.Concreate;
using Testing.Domain.Entities;

namespace Testing.Domain.Infrastructure.Test
{
    public class _Test
    {
        private DataContext context = new DataContext();

        public int AddQuestions(string title, string answer, int categoryId, string choiceOne, string choiceTwo, string choiceThree, string choiceFour) 
        {
            Question question = new Question() 
            {
                 Title = title,
                 Answer = answer,
                 CourseId = Convert.ToInt32(categoryId),
                 AddedTime = DateTime.Now,
                 ChoiceOne = choiceOne,
                 ChoiceTwo = choiceTwo,
                 ChoiceThree = choiceThree,
                 ChoiceFour = choiceFour,
                  
            };
            context.Questions.Add(question);
            context.SaveChanges();

            int id = context.Questions.FirstOrDefault(x=>x.Title == title && x.Answer == answer).QuestionId;
            return id;
        }
    }
}
