using MediatR;
using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Features.Product.Commands.ReceivePalletCommand;

public record ReceiveProductCommand(
    string OrderId,
    string PalletId,
    string CartonId,
    string ProductRef) : IRequest<DeliveryDto>;
