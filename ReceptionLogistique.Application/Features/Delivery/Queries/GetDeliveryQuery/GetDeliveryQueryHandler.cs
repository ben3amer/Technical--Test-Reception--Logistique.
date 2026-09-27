using MediatR;
using ReceptionLogistique.Application.DTOs;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Application.Mappings;

namespace ReceptionLogistique.Application.Features.Delivery.Queries.GetDeliveryQuery;

public class GetDeliveryQueryHandler(IDeliveryRepository repository)
    : IRequestHandler<GetDeliveryQuery, DeliveryDto>
{
    public async Task<DeliveryDto> Handle(GetDeliveryQuery request, CancellationToken cancellationToken)
    {
        var delivery = await repository.GetByOrderIdAsync(request.OrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"Delivery '{request.OrderId}' was not found.");

        return delivery.ToDeliveryDto();
    }
}
