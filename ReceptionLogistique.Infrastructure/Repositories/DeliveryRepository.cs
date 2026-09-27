using Microsoft.EntityFrameworkCore;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Domain.Entities;
using ReceptionLogistique.Infrastructure.Persistence;

namespace ReceptionLogistique.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ReceptionLogistiqueDbContext _context;

        public DeliveryRepository(ReceptionLogistiqueDbContext context)
        {
            _context = context;
        }

        public async Task<Delivery?> GetByOrderIdAsync(string orderId, CancellationToken cancellationToken)
        {
            return await _context.Deliveries
                .Include(d => d.Pallets)
                    .ThenInclude(p => p.Cartons)
                        .ThenInclude(c => c.Products)
                .FirstOrDefaultAsync(d => d.OrderId == orderId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}