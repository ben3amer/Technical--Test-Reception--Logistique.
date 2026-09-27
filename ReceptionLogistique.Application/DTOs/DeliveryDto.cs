using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.DTOs
{
    public record DeliveryDto(
        string OrderId,
        ReceptionStatus Status,
        ReceptionProgressDto Progress,
        IReadOnlyCollection<PalletDto> Pallets);
}
