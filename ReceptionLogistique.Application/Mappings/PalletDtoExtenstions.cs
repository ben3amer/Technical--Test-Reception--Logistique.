using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Mappings
{

    public static class PalletDtoExtensions
    {
        public static PalletDto ToPalletDto(this Domain.Entities.Pallet pallet)
        {
            return new PalletDto(
                pallet.Id,
                pallet.Status,
                [.. pallet.Cartons.Select(c => c.ToCartonDto())]);
        }
    }
}
