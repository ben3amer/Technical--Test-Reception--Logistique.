using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Infrastructure.Persistence.Configurations
{
    public class PalletConfiguration : IEntityTypeConfiguration<Pallet>
    {
        public void Configure(EntityTypeBuilder<Pallet> builder)
        {
            builder.ToTable("Pallets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasMaxLength(50)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Pallet.Cartons))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.Cartons)
                .WithOne()
                .HasForeignKey("PalletId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(p => p.Status);
        }
    }
}