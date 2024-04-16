using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly CategoryService _categoryService;
    private readonly InventoryService _inventoryService;
    private readonly PropertyService _propertyService;

    public ProductService(ProductRepository productRepository, CategoryService categoryService, InventoryService inventoryService, PropertyService propertyService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
        _inventoryService = inventoryService;
        _propertyService = propertyService;
    }

    public Product CreateProduct(ProductDto product)
    {
        var productEntity = new Product();
        try
        {
            var categoryEntity = _categoryService.CreateCategory(product.CategoryName);
            productEntity = _productRepository.Create(new Product { Title = product.Title, ProductDescription = product.ProductDescription, Price = product.Price, CategoryId = categoryEntity.Id, Inventories = product.Inventories, Properties = product.Properties});
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error: " + ex.Message);
        }
        return productEntity;
    }

    public ProductDto GetProductById(int id)
    {
        var productEntity = _productRepository.Get(x => x.Id == id);
        return productEntity;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        var products = _productRepository.GetAll();
        return products;
    }

    public Product UpdateProduct(ProductDto product)
    {
        var categoryEntity = _categoryService.CreateCategory(product.CategoryName);
        //var productEntity = new Product { Id = product.Id, Title = product.Title, ProductDescription = product.ProductDescription, Price = product.Price, CategoryId = categoryEntity.Id, Inventories = product.Inventories, Properties = product.Properties };
        foreach (var inventory in product.Inventories) {
            var existingInventory = _inventoryService.GetInventory(product.Id, inventory.StoreId);
            if (existingInventory != null) {
                _inventoryService.UpdateInventory(new Inventory { ProductId = product.Id, StoreId = inventory.StoreId, Amount = inventory.Amount});
            }
            else
            {
                _inventoryService.CreateInventory(new Inventory { ProductId = product.Id, StoreId = inventory.StoreId, Amount = inventory.Amount });
            }
        }
        //foreach (var property in product.Properties)
        //{
        //    var existingProperty = _propertyService.GetProperty(property);
        //    if (existingProperty != null)
        //    {
        //        property.Id = existingProperty.Id;
        //    }
        //    else
        //    {
        //        var newProperty = _propertyService.CreateProperty(new Property { Title = property.Title, PropertyValue = property.PropertyValue});
        //        property.Id = newProperty.Id;
        //    }
        //    //property.Products.Add(productEntity);
        //    //_propertyService.UpdateProperty(property);
        //}
        var updatedProduct = _productRepository.Update(new Product { Id = product.Id, Title = product.Title, ProductDescription = product.ProductDescription, Price = product.Price, CategoryId = categoryEntity.Id, Inventories = product.Inventories, Properties = product.Properties }, x => x.Id == product.Id);
        return updatedProduct;
    }

    public bool DeleteProduct(int id)
    {
        var result = _productRepository.Delete(x => x.Id == id);
        return result;
    }

}
