using ReceptionLogistique.Application.DTOs;

namespace ReceptionLogistique.Application.Mappings
{

    public static class ProductDtoExtensions
    {
        public static ProductDto ToProductDto(this Domain.Entities.Product product)
        {
            return new ProductDto(
                product.Ref,
                product.Name,
                product.Color,
                product.Size,
                product.ExpectedQuantity,
                product.ReceivedQuantity,
                product.Status);
        }
    }
}
