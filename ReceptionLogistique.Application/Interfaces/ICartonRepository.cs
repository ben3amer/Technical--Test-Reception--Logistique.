using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Application.Interfaces
{
    public interface ICartonRepository
    {
        Task<string> AddAsync(Carton carton);
        Task<Carton?> GetByIdAsync(string cartonId);
        Task<IEnumerable<Carton>> GetAllAsync();
        Task<string> UpdateAsync(Carton carton);
    }
}
