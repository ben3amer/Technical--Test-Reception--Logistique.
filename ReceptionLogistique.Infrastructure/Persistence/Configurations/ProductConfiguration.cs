using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Ref);

            builder.Property(p => p.Ref)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Color)
                .HasMaxLength(50);

            builder.Property(p => p.Size)
                .HasMaxLength(20);

            builder.Property(p => p.ExpectedQuantity)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();
        }
    }
}