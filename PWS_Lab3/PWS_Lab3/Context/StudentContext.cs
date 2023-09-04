using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PWS_Lab3.Models;

namespace PWS_Lab3.Context
{
    public class StudentContext : DbContext
    {
        public StudentContext() 
            : base("StudentContext") 
        { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}