using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournament");

            builder.Property(t => t.Title).HasMaxLength(50);
            builder.Property(t => t.City).HasMaxLength(50);
            builder.Property(t => t.Country).HasMaxLength(50);
        }
    }
}
