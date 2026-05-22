using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradesApp.Models;

namespace SchoolPortal.GradesApp.Data.Configurations;

public class GradeConfiguration:IEntityTypeConfiguration<Grade>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Grade> builder)
    {
        builder.Property(g => g.CourseName).HasMaxLength(100);
        builder.Property(g => g.Score)
            .HasPrecision(5, 2);
        builder.Property(g => g.Notes).HasMaxLength(500);
        builder.HasIndex(g=>g.StudentId);
    }
}