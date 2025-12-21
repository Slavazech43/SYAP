using Domain.Students;
using Domain.Students.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class LessonProgressConfiguration : IEntityTypeConfiguration<LessonProgress>
{
    public void Configure(EntityTypeBuilder<LessonProgress> builder)
    {
        builder.ToTable("lesson_progress");

        builder.HasKey(x => new { x.EnrollmentId, x.Lesson });

        builder.Property(x => x.EnrollmentId)
            .HasColumnName("enrollment_id")
            .HasConversion(v => v.Value, v => EnrollmentId.Create(v))
            .IsRequired();

        builder.Property(x => x.Lesson)
            .HasColumnName("lesson_id")
            .HasConversion(v => v.Value, v => LessonRef.Create(v))
            .IsRequired();

        builder.Property(x => x.Percent)
            .HasColumnName("progress_percent")
            .HasConversion(v => v.Value, v => ProgressPercent.Create(v))
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .HasColumnName("is_completed")
            .IsRequired();
    }
}
