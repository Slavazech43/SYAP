using Domain.Courses;
using Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("tests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(v => v.Value, v => TestId.Create(v))
            .ValueGeneratedNever();

        builder.HasMany<Question>("_questions")
            .WithOne()
            .HasForeignKey("test_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Questions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
