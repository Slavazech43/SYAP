using Domain.Courses;
using Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("lessons");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(v => v.Value, v => LessonId.Create(v))
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .HasConversion(v => v.Value, v => LessonTitle.Create(v))
            .IsRequired();

        builder.HasOne(x => x.Test)
            .WithOne()
            .HasForeignKey<Test>("lesson_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
