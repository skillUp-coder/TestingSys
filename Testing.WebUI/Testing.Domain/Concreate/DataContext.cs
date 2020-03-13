using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Domain.Concreate
{
    public class DataContext:DbContext
    {
        public DataContext():base()
        {}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        
    }
}
