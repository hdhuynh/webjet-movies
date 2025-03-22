using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webjet.Domain.Employees;

namespace Webjet.Infrastructure.Persistence.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(e => e.Id)
            .IsClustered(false);

        builder.Property(e => e.Id)
            .HasColumnName("RegionID")
            .HasConversion(e => e.Value, e => new RegionId(e))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.RegionDescription)
            .IsRequired()
            .HasMaxLength(50);
    }
}