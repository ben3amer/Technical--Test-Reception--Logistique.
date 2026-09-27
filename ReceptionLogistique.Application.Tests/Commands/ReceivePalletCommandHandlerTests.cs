using NSubstitute;
using ReceptionLogistique.Application.Features.Pallet.Commands.ReceivePalletCommand;
using ReceptionLogistique.Application.Interfaces;
using ReceptionLogistique.Domain.Entities;
using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Application.Tests.Commands;

public class ReceivePalletCommandHandlerTests
{
    private readonly IDeliveryRepository _repository = Substitute.For<IDeliveryRepository>();
    private readonly ReceivePalletCommandHandler _handler;

    public ReceivePalletCommandHandlerTests()
    {
        _handler = new ReceivePalletCommandHandler(_repository);
    }

    private static Delivery BuildDelivery()
    {
        var product = new Product("REF-1", "Widget", "Red", "M", 10);
        var carton = new Carton("CTN-1", [product]);
        var pallet = new Pallet("PLT-1", [carton]);
        return new Delivery("ORD-001", [pallet]);
    }

    [Fact]
    public async Task Handle_ValidPallet_ReceivesPalletAndReturnsDto()
    {
        var delivery = BuildDelivery();
        _repository.GetByOrderIdAsync("ORD-001", default).Returns(delivery);

        var result = await _handler.Handle(new ReceivePalletCommand("ORD-001", "PLT-1"), default);

        Assert.Equal(ReceptionStatus.Received, result.Status);
        await _repository.Received(1).SaveChangesAsync(default);
    }

    [Fact]
    public async Task Handle_DeliveryNotFound_ThrowsKeyNotFoundException()
    {
        _repository.GetByOrderIdAsync("MISSING", default).Returns((Delivery?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _handler.Handle(new ReceivePalletCommand("MISSING", "PLT-1"), default));
    }
}
