using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Domain.Entities
{
    public class Test
    {
        public int TestId {get;set;}
        public string TestQuestion { get; set; }
        public string Answer { get; set; }
        //public int? CourseId { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public Test()
        {
            Courses = new List<Course>();
        }
    }
}
