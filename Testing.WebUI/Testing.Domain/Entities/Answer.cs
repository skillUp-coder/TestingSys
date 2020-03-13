using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Domain.Entities
{
    public class Answer
    {
        
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public int _QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
