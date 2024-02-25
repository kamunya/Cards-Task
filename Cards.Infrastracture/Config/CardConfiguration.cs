using Cards.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastracture.Config
{
    internal class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(70);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Color).HasMaxLength(7);
            builder.Property(p => p.Status);
        }
    }
}
