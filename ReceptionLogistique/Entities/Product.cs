using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Domain.Entities
{
    public class Product
    {
        private Product() { }

        public Product(
            string reference,
            string name,
            string color,
            string size,
            int expectedQuantity)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(reference);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (expectedQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(expectedQuantity));

            Ref = reference;
            Name = name;
            Color = color;
            Size = size;
            ExpectedQuantity = expectedQuantity;
        }

        public string Ref { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string Color { get; private set; } = null!;
        public string Size { get; private set; } = null!;
        public int ExpectedQuantity { get; private set; }

        public int ReceivedQuantity { get; private set; }

        public ReceptionStatus Status
        {
            get
            {
                if (ReceivedQuantity == 0)
                {
                    return ReceptionStatus.NotReceived;
                }

                if (ReceivedQuantity >= ExpectedQuantity)
                {
                    return ReceptionStatus.Received;
                }

                return ReceptionStatus.PartiallyReceived;
            }
        }

        public void Receive()
        {
            ReceivedQuantity = ExpectedQuantity;
        }

        public void Unreceive()
        {
            ReceivedQuantity = 0;
        }
    }
}
