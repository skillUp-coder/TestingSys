using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Domain.Entities
{
    public class Exam
    {
        public int ExamId { get; set; }
        public int Point { get; set; }
        public int TrueCounter { get; set; }
        public int FalseCounter { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        public int? CourseId { get; set; }
    }
}
