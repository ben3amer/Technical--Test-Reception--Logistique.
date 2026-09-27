using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string productId, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<string> AddAsync(Product product, CancellationToken cancellationToken);
        Task<string> UpdateAsync(Product product, CancellationToken cancellationToken);
    }
}
