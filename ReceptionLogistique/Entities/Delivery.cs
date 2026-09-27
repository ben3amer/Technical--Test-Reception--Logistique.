using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Domain.Entities
{
    public class Delivery
    {
        private readonly List<Pallet> _pallets = [];

        public string OrderId { get; private set; } = null!;

        public IReadOnlyCollection<Pallet> Pallets => _pallets;

        public ReceptionStatus Status
        {
            get
            {
                if (_pallets.All(x => x.Status == ReceptionStatus.Received))
                    return ReceptionStatus.Received;

                if (_pallets.All(x => x.Status == ReceptionStatus.NotReceived))
                    return ReceptionStatus.NotReceived;

                return ReceptionStatus.PartiallyReceived;
            }
        }
    }
}
