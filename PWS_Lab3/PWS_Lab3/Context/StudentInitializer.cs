using System.Collections.Generic;
using System.Data.Entity;
using PWS_Lab3.Models;

namespace PWS_Lab3.Context
{
    public class StudentInitializer : DropCreateDatabaseIfModelChanges<StudentContext>
    {
        protected override void Seed(StudentContext context)
        {
            var students = new List<Student>
        {
            new Student { Id = 1, Name = "Valdaitsev Alexander", Phone = "+375445573401" },
            new Student { Id = 2, Name = "Vrublevskaya Katie", Phone = "+375333847194" },
            new Student { Id = 3, Name = "Gud Vladislav", Phone = "+375294823753" },
            new Student { Id = 4, Name = "Dimitriardi Anthony", Phone = "+375339023757" },
            new Student { Id = 5, Name = "Tkachou Anton", Phone = "+375448293053" },
            new Student { Id = 6, Name = "Budanova Xenia", Phone = "+375333910385" }
        };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
        }
    }
}