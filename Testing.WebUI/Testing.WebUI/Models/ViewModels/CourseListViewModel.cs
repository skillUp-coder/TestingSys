using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Testing.Domain.Entities;

namespace Testing.WebUI.Models.ViewModels
{
    public class CourseListViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        
        public PageInfo pageInfo { get; set; }
        public string CurrentCategory { get; set; }
        
    }
}