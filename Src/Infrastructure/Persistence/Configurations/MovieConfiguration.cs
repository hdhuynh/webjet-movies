using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webjet.Domain.Categories;
using Webjet.Domain.Movies;

namespace Webjet.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("MovieID")
            .HasConversion(e => e.Value, e => new MovieId(e))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Poster)
            .HasColumnType("ntext");
    }
}