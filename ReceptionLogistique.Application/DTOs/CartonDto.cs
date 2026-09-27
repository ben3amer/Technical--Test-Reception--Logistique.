using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.DTOs
{
    public record CartonDto(
        string Id,
        ReceptionStatus Status,
        IReadOnlyCollection<ProductDto> Products);
}
