using Infrastructure.Entities;
using Infrastructure.Services;

namespace Infrastructure.Dtos;

public class ProductDto
{

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal Price { get; set; }

    public string CategoryName { get; set; } = null!;

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory> { };

    public ICollection<Property> Properties { get; set; } = new List<Property> { };

    public static implicit operator ProductDto(Product product)
    {
        var productDto = new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            ProductDescription = product.ProductDescription,
            Price = product.Price,
            CategoryName = product.Category.CategoryName,
            Inventories = product.Inventories,
            Properties = product.Properties
        };
        return productDto;
    }
}