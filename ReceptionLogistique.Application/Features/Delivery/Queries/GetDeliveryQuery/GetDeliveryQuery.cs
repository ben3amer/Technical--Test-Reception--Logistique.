using MediatR;
using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Features.Delivery.Queries.GetDeliveryQuery;

public record GetDeliveryQuery(string OrderId) : IRequest<DeliveryDto>;
