using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Abstract
{
    public interface ICourseRepository
    {
        IEnumerable<Course> Courses { get; set; }
    }
}
