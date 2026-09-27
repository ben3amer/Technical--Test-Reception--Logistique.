using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.DTOs
{
    public record ProductDto(
        string Ref,
        string Name,
        string Color,
        string Size,
        int ExpectedQuantity,
        int ReceivedQuantity,
        ReceptionStatus Status);
}
