using Domain.Courses;
using Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("modules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(v => v.Value, v => ModuleId.Create(v))
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasMaxLength(ModuleTitle.MaxLength)
            .HasConversion(v => v.Value, v => ModuleTitle.Create(v))
            .IsRequired();

        builder.HasMany<Lesson>("_lessons")
            .WithOne()
            .HasForeignKey("module_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Lessons)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
