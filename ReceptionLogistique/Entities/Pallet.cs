using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Domain.Entities
{
    public class Pallet
    {
        private readonly List<Carton> _cartons;

        public Pallet(string id, IEnumerable<Carton> cartons)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(id);
            ArgumentNullException.ThrowIfNull(cartons);

            Id = id;
            _cartons = [.. cartons];

            if (_cartons.Count == 0)
                throw new ArgumentException("A pallet must contain at least one carton.", nameof(cartons));
        }

        public string Id { get; private set; }

        public IReadOnlyCollection<Carton> Cartons => _cartons;

        public ReceptionStatus Status
        {
            get
            {
                if (_cartons.All(c => c.Status == ReceptionStatus.Received))
                    return ReceptionStatus.Received;

                if (_cartons.All(c => c.Status == ReceptionStatus.NotReceived))
                    return ReceptionStatus.NotReceived;

                return ReceptionStatus.PartiallyReceived;
            }
        }

        public void Receive()
        {
            foreach (var carton in _cartons)
            {
                carton.Receive();
            }
        }

        public void ReceiveCarton(string cartonId)
        {
            var carton = _cartons.Single(c => c.Id == cartonId);
            carton.Receive();
        }
    }
}
