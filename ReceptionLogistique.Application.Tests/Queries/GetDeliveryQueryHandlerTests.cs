using NSubstitute;
using ReceptionLogistique.Application.Features.Delivery.Queries.GetDeliveryQuery;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Domain.Entities;

namespace ReceptionLogistique.Application.Tests.Queries;

public class GetDeliveryQueryHandlerTests
{
    private readonly IDeliveryRepository _repository = Substitute.For<IDeliveryRepository>();
    private readonly GetDeliveryQueryHandler _handler;

    public GetDeliveryQueryHandlerTests()
    {
        _handler = new GetDeliveryQueryHandler(_repository);
    }

    private static Delivery BuildDelivery(string orderId = "ORD-001")
    {
        var product = new Product("REF-1", "Widget", "Red", "M", 10);
        var carton = new Carton("CTN-1", [product]);
        var pallet = new Pallet("PLT-1", [carton]);
        return new Delivery(orderId, [pallet]);
    }

    [Fact]
    public async Task Handle_DeliveryExists_ReturnsDeliveryDto()
    {
        var delivery = BuildDelivery();
        _repository.GetByOrderIdAsync("ORD-001", default).Returns(delivery);

        var result = await _handler.Handle(new GetDeliveryQuery("ORD-001"), default);

        Assert.Equal("ORD-001", result.OrderId);
        Assert.Single(result.Pallets);
    }

    [Fact]
    public async Task Handle_DeliveryNotFound_ThrowsKeyNotFoundException()
    {
        _repository.GetByOrderIdAsync("MISSING", default).Returns((Delivery?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(new GetDeliveryQuery("MISSING"), default));
    }
}
