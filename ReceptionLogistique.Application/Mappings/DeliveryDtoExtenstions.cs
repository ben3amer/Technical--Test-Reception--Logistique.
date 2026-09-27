using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Mappings
{

    public static class DeliveryDtoExtensions
    {
        public static DeliveryDto ToDeliveryDto(this Domain.Entities.Delivery delivery)
        {
            return new DeliveryDto(
                delivery.OrderId,
                delivery.Status,
                delivery.Progress.ToReceptionProgressDto(),
                [.. delivery.Pallets.Select(p => p.ToPalletDto())]);
        }
    }
}
