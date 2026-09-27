using MediatR;
using ReceptionLogistique.Application.DTOs;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Application.Mappings;

namespace ReceptionLogistique.Application.Features.Product.Commands.UnreceiveProductCommand;

public class UnreceiveProductCommandHandler(IDeliveryRepository repository)
    : IRequestHandler<UnreceiveProductCommand, DeliveryDto>
{
    public async Task<DeliveryDto> Handle(UnreceiveProductCommand request, CancellationToken cancellationToken)
    {
        var delivery = await repository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"Delivery '{request.OrderId}' was not found.");

        delivery.UnreceiveProduct(request.PalletId, request.CartonId, request.ProductRef);
        await repository.SaveAsync(delivery, cancellationToken);

        return delivery.ToDeliveryDto();
    }
}
