using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradesApp.Models;

namespace SchoolPortal.GradesApp.Data;

public class GradeDbContext:DbContext
{
    public GradeDbContext(DbContextOptions<GradeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GradeDbContext).Assembly);
    }
    public DbSet<Grade> Grades { get; set; } = null!;
    
}
