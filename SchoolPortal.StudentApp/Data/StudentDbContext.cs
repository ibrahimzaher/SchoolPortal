using Microsoft.EntityFrameworkCore;
using SchoolPortal.StudentApp.Models;

namespace SchoolPortal.StudentApp.Data;

public class StudentDbContext:DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentDbContext).Assembly);
    }
    public DbSet<Student> Students { get; set; } = null!;
}
