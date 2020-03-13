using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Abstract
{
    public interface ITestRepository
    {
        IEnumerable<Answer> Answers { get; set; }
        IEnumerable<Question> Questions { get; set; }
    }
}
