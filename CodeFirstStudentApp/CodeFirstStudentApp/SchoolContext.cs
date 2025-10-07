using System.Data.Entity;

namespace CodeFirstStudentApp
{
    public class SchoolContext : DbContext
    {
        // Name matches App.config connection string
        public SchoolContext() : base("name=SchoolContext")
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
