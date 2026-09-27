using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Infrastructure.Persistence.Seed
{
    public static class DeliverySeeder
    {
        public static void Seed(ReceptionLogistiqueDbContext context)
        {
            if (context.Deliveries.Any())
                return;

            var products1 = new List<Product>
            {
                new("TSH-RED-M", "T-Shirt Sport", "Rouge", "M", 50),
                new("SHO-BLK-42", "Baskets Running", "Noir", "42", 10)
            };

            var carton1 = new Carton("CART-01-A", products1);
            var pallet1 = new Pallet("PAL-01", [carton1]);
            var delivery = new Delivery("CMD-2026", [pallet1]);

            context.Deliveries.Add(delivery);
            context.SaveChanges();
        }
    }
}