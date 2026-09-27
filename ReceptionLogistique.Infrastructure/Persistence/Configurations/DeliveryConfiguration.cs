using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Infrastructure.Persistence.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Deliveries");

            builder.HasKey(d => d.OrderId);

            builder.Property(d => d.OrderId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Delivery.Pallets))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(d => d.Pallets)
                .WithOne()
                .HasForeignKey("DeliveryOrderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(d => d.Status);
            builder.Ignore(d => d.Progress);
        }
    }
}