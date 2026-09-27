using NSubstitute;
using ReceptionLogistique.Application.Features.Product.Commands.UnreceiveProductCommand;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Domain.Entities;
using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.Tests.Commands;

public class UnreceiveProductCommandHandlerTests
{
    private readonly IDeliveryRepository _repository = Substitute.For<IDeliveryRepository>();
    private readonly UnreceiveProductCommandHandler _handler;

    public UnreceiveProductCommandHandlerTests()
    {
        _handler = new UnreceiveProductCommandHandler(_repository);
    }

    private static Delivery BuildDeliveryWithReceivedProduct()
    {
        var product = new Product("REF-1", "Widget", "Red", "M", 10);
        var carton = new Carton("CTN-1", [product]);
        var pallet = new Pallet("PLT-1", [carton]);
        var delivery = new Delivery("ORD-001", [pallet]);
        // receive it first so we can unreceive it
        delivery.ReceiveProduct("PLT-1", "CTN-1", "REF-1");
        return delivery;
    }

    [Fact]
    public async Task Handle_ReceivedProduct_UnreceivesAndReturnsDto()
    {
        var delivery = BuildDeliveryWithReceivedProduct();
        _repository.GetByOrderIdAsync("ORD-001", default).Returns(delivery);

        var result = await _handler.Handle(
            new UnreceiveProductCommand("ORD-001", "PLT-1", "CTN-1", "REF-1"), default);

        var product = result.Pallets.Single().Cartons.Single().Products.Single();
        Assert.Equal(ReceptionStatus.NotReceived, product.Status);
        await _repository.Received(1).SaveChangesAsync(default);
    }

    [Fact]
    public async Task Handle_DeliveryNotFound_ThrowsKeyNotFoundException()
    {
        _repository.GetByOrderIdAsync("MISSING", default).Returns((Delivery?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(new UnreceiveProductCommand("MISSING", "PLT-1", "CTN-1", "REF-1"), default));
    }
}
