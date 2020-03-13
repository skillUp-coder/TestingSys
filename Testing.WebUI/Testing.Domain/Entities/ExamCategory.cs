using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Domain.Entities
{
    public class ExamCategory
    {
        public int ExamCategoryId { get; set; }
        public int TrueCounter { get; set; }
        public int FalseCounter { get; set; }
        public int CategoryId { get; set; }
        public int ExamId { get; set; }
    }
}
