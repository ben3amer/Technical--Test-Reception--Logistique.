using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Mappings
{
    public static class CartonDtoExtensions
    {
        public static CartonDto ToCartonDto(this Domain.Entities.Carton carton)
        {
            return new CartonDto(
                carton.Id,
                carton.Status,
                [.. carton.Products.Select(p => p.ToProductDto())]);
        }
    }
}
