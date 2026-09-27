using ReceptionLogistique.Application.DTOs;
using ReceptionLogistique.Domain.ValueObjects;

namespace ReceptionLogistique.Application.Mappings
{
    public static class ReceptionProgressDtoExtensions
    {
        public static ReceptionProgressDto ToReceptionProgressDto(this ReceptionProgress progress)
        {
            return new ReceptionProgressDto(progress.TotalItems, progress.ReceivedItems);
        }
    }
}
