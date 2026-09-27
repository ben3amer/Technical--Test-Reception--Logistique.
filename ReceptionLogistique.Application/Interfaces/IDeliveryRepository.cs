using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Application.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<Delivery?> GetByOrderIdAsync(string orderId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}