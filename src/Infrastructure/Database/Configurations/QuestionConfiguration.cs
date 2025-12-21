using Domain.Courses;
using Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(v => v.Value, v => QuestionId.Create(v))
            .ValueGeneratedNever();

        builder.Property(x => x.Text)
            .HasMaxLength(500)
            .HasConversion(v => v.Value, v => QuestionText.Create(v))
            .IsRequired();

        builder.Property(x => x.CorrectAnswer)
            .HasMaxLength(500)
            .HasConversion(v => v.Value, v => AnswerText.Create(v))
            .IsRequired();
    }
}
