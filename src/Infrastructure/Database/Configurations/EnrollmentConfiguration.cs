using Domain.Common;
using Domain.Students;
using Domain.Students.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(v => v.Value, v => EnrollmentId.Create(v))
            .ValueGeneratedNever();

        builder.Property(x => x.Course)
            .HasColumnName("course_id")
            .HasConversion(v => v.Value, v => CourseRef.Create(v))
            .IsRequired();

        builder.Property(x => x.EnrolledAtUtc)
            .HasColumnName("enrolled_at_utc")
            .HasConversion(v => v.Value, v => UtcDateTime.Create(v))
            .IsRequired();

        builder.HasMany<LessonProgress>("_progress")
            .WithOne()
            .HasForeignKey("enrollment_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<TestAttempt>("_attempts")
            .WithOne()
            .HasForeignKey("enrollment_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Progress)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Attempts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
