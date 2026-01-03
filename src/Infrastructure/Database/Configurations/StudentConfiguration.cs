using Domain.Common;
using Domain.Students;
using Domain.Students.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(v => v.Value, v => StudentId.Create(v))
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(PersonName.MaxLength)
            .HasConversion(v => v.Value, v => PersonName.Create(v))
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(Email.MaxLength)
            .HasConversion(v => v.Value, v => Email.Create(v))
            .IsRequired();

        builder.HasMany<Enrollment>("_enrollments")
            .WithOne()
            .HasForeignKey("student_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Enrollments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
