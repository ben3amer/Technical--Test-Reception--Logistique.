using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Application.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<Delivery?> GetByIdAsync(string orderId, CancellationToken cancellationToken);
        Task SaveAsync(Delivery delivery, CancellationToken cancellationToken);
        Task<string> AddAsync(Delivery delivery, CancellationToken cancellationToken);
        Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken cancellationToken);
        Task<string> UpdateAsync(Delivery delivery, CancellationToken cancellationToken);
    }
}
