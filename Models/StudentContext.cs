
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace API_FinalTask.Models
{
    public class StudentContext:IdentityDbContext
    {
        public StudentContext(DbContextOptions<StudentContext> option) : base(option) { }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }
}
