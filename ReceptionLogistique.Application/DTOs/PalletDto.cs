using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.DTOs
{
    public record PalletDto(
        string Id,
        ReceptionStatus Status,
        IReadOnlyCollection<CartonDto> Cartons);
}
