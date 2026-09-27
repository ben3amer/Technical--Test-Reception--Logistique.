using MediatR;
using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Features.Product.Commands.UnreceiveProductCommand;

public record UnreceiveProductCommand(
    string OrderId,
    string PalletId,
    string CartonId,
    string ProductRef) : IRequest<DeliveryDto>;
