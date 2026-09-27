using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Domain.Entities
{
    public class Carton
    {
        private readonly List<Product> _products;

        public Carton(string id, IEnumerable<Product> products)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(id);
            ArgumentNullException.ThrowIfNull(products);

            Id = id;
            _products = [.. products];

            if (_products.Count == 0)
                throw new ArgumentException("A carton must contain at least one product.", nameof(products));
        }

        public string Id { get; private set; }

        public IReadOnlyCollection<Product> Products => _products;

        public ReceptionStatus Status
        {
            get
            {
                if (_products.All(x => x.Status == ReceptionStatus.Received))
                    return ReceptionStatus.Received;

                if (_products.All(x => x.Status == ReceptionStatus.NotReceived))
                    return ReceptionStatus.NotReceived;

                return ReceptionStatus.PartiallyReceived;
            }
        }

        public void Receive()
        {
            foreach (var product in _products)
                product.Receive();
        }

        public void ReceiveProduct(string productRef)
        {
            var product = _products.Single(x => x.Ref == productRef);
            product.Receive();
        }

        public void UnreceiveProduct(string productRef)
        {
            var product = _products.Single(x => x.Ref == productRef);
            product.Unreceive();
        }
    }
}
