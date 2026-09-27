using MediatR;
using ReceptionLogistique.Application.DTOs;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Application.Mappings;

namespace ReceptionLogistique.Application.Features.Carton.Commands.ReceiveCartonCommand;

public class ReceiveCartonCommandHandler(IDeliveryRepository repository)
    : IRequestHandler<ReceiveCartonCommand, DeliveryDto>
{
    public async Task<DeliveryDto> Handle(ReceiveCartonCommand request, CancellationToken cancellationToken)
    {
        var delivery = await repository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"Delivery '{request.OrderId}' was not found.");

        delivery.ReceiveCarton(request.PalletId, request.CartonId);
        await repository.SaveAsync(delivery, cancellationToken);

        return delivery.ToDeliveryDto();
    }
}
