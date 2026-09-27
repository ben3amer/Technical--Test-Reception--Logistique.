using MediatR;
using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Features.Carton.Commands.ReceiveCartonCommand;

public record ReceiveCartonCommand(string OrderId, string PalletId, string CartonId) : IRequest<DeliveryDto>;
