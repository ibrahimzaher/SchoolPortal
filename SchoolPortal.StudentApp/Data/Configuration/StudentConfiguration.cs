using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.StudentApp.Models;

namespace SchoolPortal.StudentApp.Data.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.FirstName)
            .HasMaxLength(100);

        builder.Property(s => s.LastName)
            .HasMaxLength(100);
        builder.Property(s => s.Email)
            .HasMaxLength(30);
        builder.HasIndex(s=>s.Email)
            .IsUnique();
    }
}
