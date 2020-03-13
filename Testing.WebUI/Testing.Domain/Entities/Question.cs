using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Domain.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public string ChoiceOne { get; set; }
        public string ChoiceTwo { get; set; }
        public string ChoiceThree { get; set; }
        public string ChoiceFour { get; set; }
        public int CourseId { get; set; }
        public DateTime AddedTime { get; set; }
        public Course Course { get; set; }
        public int Count { get; set; }
        
        



        
    }
}
