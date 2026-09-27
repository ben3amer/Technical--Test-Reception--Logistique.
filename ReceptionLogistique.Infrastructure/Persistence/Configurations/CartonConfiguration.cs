using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Infrastructure.Persistence.Configurations
{
    public class CartonConfiguration : IEntityTypeConfiguration<Carton>
    {
        public void Configure(EntityTypeBuilder<Carton> builder)
        {
            builder.ToTable("Cartons");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasMaxLength(50)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Carton.Products))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Products)
                .WithOne()
                .HasForeignKey("CartonId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(c => c.Status);
        }
    }
}