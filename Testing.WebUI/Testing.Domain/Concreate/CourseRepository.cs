using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Domain.Abstract;
using Testing.Domain.Entities;

namespace Testing.Domain.Concreate
{
    public class CourseRepository : ICourseRepository
    {
        private DataContext context;
        public CourseRepository()
        {
            context = new DataContext();
        }
        public IEnumerable<Course> Courses { get { return context.Courses;  } set { } }
    }
}
