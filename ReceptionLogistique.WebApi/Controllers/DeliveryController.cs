using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReceptionLogistique.Application.Features.Carton.Commands.ReceiveCartonCommand;
using ReceptionLogistique.Application.Features.Delivery.Queries.GetDeliveryQuery;
using ReceptionLogistique.Application.Features.Pallet.Commands.ReceivePalletCommand;
using ReceptionLogistique.Application.Features.Product.Commands.ReceivePalletCommand;
using ReceptionLogistique.Application.Features.Product.Commands.UnreceiveProductCommand;

namespace ReceptionLogistique.WebApi.Controllers;

[ApiController]
[Route("delivery")]
public class DeliveryController(IMediator mediator) : ControllerBase
{
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetDelivery(string orderId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetDeliveryQuery(orderId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{orderId}/pallets/{palletId}/receive")]
    public async Task<IActionResult> ReceivePallet(string orderId, string palletId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ReceivePalletCommand(orderId, palletId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{orderId}/pallets/{palletId}/cartons/{cartonId}/receive")]
    public async Task<IActionResult> ReceiveCarton(string orderId, string palletId, string cartonId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ReceiveCartonCommand(orderId, palletId, cartonId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{orderId}/pallets/{palletId}/cartons/{cartonId}/products/{productRef}/receive")]
    public async Task<IActionResult> ReceiveProduct(string orderId, string palletId, string cartonId, string productRef, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ReceiveProductCommand(orderId, palletId, cartonId, productRef), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{orderId}/pallets/{palletId}/cartons/{cartonId}/products/{productRef}/receive")]
    public async Task<IActionResult> UnreceiveProduct(string orderId, string palletId, string cartonId, string productRef, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UnreceiveProductCommand(orderId, palletId, cartonId, productRef), cancellationToken);
        return Ok(result);
    }
}

