using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Application.Interfaces
{
    public interface IPalletRepository
    {
        Task<Pallet> GetByIdAsync(string palletId, CancellationToken cancellationToken);
        Task<IEnumerable<Pallet>> GetAllAsync(CancellationToken cancellationToken);
        Task<string> AddAsync(Pallet pallet, CancellationToken cancellationToken);
        Task<string> UpdateAsync(Pallet pallet, CancellationToken cancellationToken);

    }
}
