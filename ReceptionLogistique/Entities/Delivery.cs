using ReceptionLogistique.Domain.Enums;
using ReceptionLogistique.Domain.ValueObjects;

namespace ReceptionLogistique.Domain.Entities
{
    public class Delivery
    {
        private readonly List<Pallet> _pallets;

        public Delivery(string orderId, IEnumerable<Pallet> pallets)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(orderId);
            ArgumentNullException.ThrowIfNull(pallets);

            OrderId = orderId;
            _pallets = [.. pallets];

            if (_pallets.Count == 0)
            {
                throw new ArgumentException("A delivery must contain at least one pallet.", nameof(pallets));
            }
        }

        public string OrderId { get; private set; }

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

        public void ReceiveProduct(
            string palletId,
            string cartonId,
            string productRef)
        {
            var pallet = _pallets.Single(p => p.Id == palletId);

            var carton = pallet.Cartons
                .Single(c => c.Id == cartonId);

            carton.ReceiveProduct(productRef);
        }

        public void ReceivePallet(string palletId)
        {
            var pallet = _pallets.Single(p => p.Id == palletId);
            pallet.Receive();
        }

        public void ReceiveCarton(string palletId, string cartonId)
        {
            var pallet = _pallets.Single(p => p.Id == palletId);
            pallet.ReceiveCarton(cartonId);
        }

        public void UnreceiveProduct(
            string palletId,
            string cartonId,
            string productRef)
        {
            var pallet = _pallets.Single(p => p.Id == palletId);

            var carton = pallet.Cartons
                .Single(c => c.Id == cartonId);

            carton.UnreceiveProduct(productRef);
        }

        public ReceptionProgress Progress
        {
            get
            {
                var allProducts = _pallets
                    .SelectMany(p => p.Cartons)
                    .SelectMany(c => c.Products)
                    .ToList();

                var total = allProducts.Count;
                var received = allProducts.Count(p => p.Status == ReceptionStatus.Received);

                return new ReceptionProgress(total, received);
            }
        }
    }
}
