using MediatR;
using ReceptionLogistique.Application.DTOs;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Application.Mappings;

namespace ReceptionLogistique.Application.Features.Pallet.Commands.ReceivePalletCommand;

public class ReceivePalletCommandHandler(IDeliveryRepository repository)
    : IRequestHandler<ReceivePalletCommand, DeliveryDto>
{
    public async Task<DeliveryDto> Handle(ReceivePalletCommand request, CancellationToken cancellationToken)
    {
        var delivery = await repository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"Delivery '{request.OrderId}' was not found.");

        delivery.ReceivePallet(request.PalletId);
        await repository.SaveAsync(delivery, cancellationToken);

        return delivery.ToDeliveryDto();
    }
}
