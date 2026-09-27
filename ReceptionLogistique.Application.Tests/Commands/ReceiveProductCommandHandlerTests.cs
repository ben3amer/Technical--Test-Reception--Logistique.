using NSubstitute;
using ReceptionLogistique.Application.Features.Product.Commands.ReceivePalletCommand;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Domain.Entities;
using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.Tests.Commands;

public class ReceiveProductCommandHandlerTests
{
    private readonly IDeliveryRepository _repository = Substitute.For<IDeliveryRepository>();
    private readonly ReceiveProductCommandHandler _handler;

    public ReceiveProductCommandHandlerTests()
    {
        _handler = new ReceiveProductCommandHandler(_repository);
    }

    private static Delivery BuildDelivery()
    {
        var product = new Product("REF-1", "Widget", "Red", "M", 10);
        var carton = new Carton("CTN-1", [product]);
        var pallet = new Pallet("PLT-1", [carton]);
        return new Delivery("ORD-001", [pallet]);
    }

    [Fact]
    public async Task Handle_ValidProduct_ReceivesProductAndReturnsDto()
    {
        var delivery = BuildDelivery();
        _repository.GetByOrderIdAsync("ORD-001", default).Returns(delivery);

        var result = await _handler.Handle(
            new ReceiveProductCommand("ORD-001", "PLT-1", "CTN-1", "REF-1"), default);

        var product = result.Pallets.Single().Cartons.Single().Products.Single();
        Assert.Equal(ReceptionStatus.Received, product.Status);
        await _repository.Received(1).SaveChangesAsync(default);
    }

    [Fact]
    public async Task Handle_DeliveryNotFound_ThrowsKeyNotFoundException()
    {
        _repository.GetByOrderIdAsync("MISSING", default).Returns((Delivery?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(new ReceiveProductCommand("MISSING", "PLT-1", "CTN-1", "REF-1"), default));
    }
}
