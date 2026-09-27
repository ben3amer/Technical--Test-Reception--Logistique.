using MediatR;
using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Features.Pallet.Commands.ReceivePalletCommand;

public record ReceivePalletCommand(string OrderId, string PalletId) : IRequest<DeliveryDto>;
