using Domain.Students;
using Domain.Students.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Configurations;

public sealed class TestAttemptConfiguration : IEntityTypeConfiguration<TestAttempt>
{
    private static readonly ValueConverter<AttemptStatus, int> _statusConverter =
        new ValueConverter<AttemptStatus, int>(
            v => v.Key,
            v => AttemptStatus.FromKey(v)
        );

    private static readonly ValueConverter<ScorePercent, int> _scoreConverter =
        new ValueConverter<ScorePercent, int>(
            v => v.Value,
            v => ScorePercent.Create(v)
        );

    public void Configure(EntityTypeBuilder<TestAttempt> builder)
    {
        builder.ToTable("test_attempts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(v => v.Value, v => AttemptId.Create(v))
            .ValueGeneratedNever();

        builder.Property(x => x.Test)
            .HasColumnName("test_id")
            .HasConversion(v => v.Value, v => TestRef.Create(v))
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status_key")
            .HasConversion(_statusConverter)
            .IsRequired();

        builder.Property(x => x.Score)
            .HasColumnName("score_percent")
            .HasConversion(_scoreConverter)
            .IsRequired();
    }
}
