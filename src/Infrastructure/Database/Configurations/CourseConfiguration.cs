using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                v => v.Value,
                v => CourseId.Create(v)
            )
            .ValueGeneratedNever();

       
        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .HasConversion(
                v => v.Value,
                v => CourseTitle.Create(v)
            )
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(2000)
            .HasConversion(
                v => v.Value,
                v => CourseDescription.Create(v)
            )
            .IsRequired();

        builder.OwnsOne(x => x.Price, money =>
        {
            money.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            money.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        
        builder.Property(x => x.Status)
            .HasColumnName("status_key")
            .HasConversion(
                v => v.Key,
                v => CourseStatus.FromKey(v)
            )
            .IsRequired();

       builder.HasMany<Module>("_modules")
            .WithOne()
            .HasForeignKey("course_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Modules)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
