using ReceptionLogistique.Domain.Enums;

namespace ReceptionLogistique.Domain.Entities
{
    public class Pallet
    {
        private readonly List<Carton> _cartons = [];

        public string Id { get; private set; } = null!;

        public IReadOnlyCollection<Carton> Cartons => _cartons;

        public ReceptionStatus Status
        {
            get
            {
                if (_cartons.All(x => x.Status == ReceptionStatus.Received))
                    return ReceptionStatus.Received;

                if (_cartons.All(x => x.Status == ReceptionStatus.NotReceived))
                    return ReceptionStatus.NotReceived;

                return ReceptionStatus.PartiallyReceived;
            }
        }

        public void Receive()
        {
            foreach (var carton in _cartons)
                carton.Receive();
        }
    }
}
