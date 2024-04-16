using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class ProductService_Tests
{

    private readonly DataContext _context = new DataContext(
      new DbContextOptionsBuilder<DataContext>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void CreateProduct_Should_Add_One_Product_ToDB_And_Return_New_Product()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        var productDto = new ProductDto { Title = "Test Product", ProductDescription = "Test Description", Price = 1.0000M, CategoryName = "Test Category" };

        // Act
        var result = productService.CreateProduct(productDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Product>(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(productDto.CategoryName, result.Category.CategoryName);
        Assert.Equal(productDto.ProductDescription, result.ProductDescription);
        Assert.Equal(productDto.Price, result.Price);
    }

    [Fact]
    public void CreateProduct_Should_Use_Existing_CategoryEntity_If_Provided_CategoryName_Already_Exists_InDb()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        string categoryName = "Test Category";
        var categoryEntity = categoryService.CreateCategory(categoryName);
        var productDto = new ProductDto { Title = "Test Product", ProductDescription = "Test Description", Price = 1.0000M, CategoryName = categoryName };

        // Act
        var result = productService.CreateProduct(productDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.Id, result.CategoryId);
    }

    [Fact]
    public void GetProductById_Should_Find_Existing_Poduct_By_Id_And_Return_Product_As_ProductDto()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        var productEntity = productService.CreateProduct(new ProductDto { Title = "Test Product", ProductDescription = "Test Description", Price = 1.0000M, CategoryName = "Test Category" });

        // Act
        var result = productService.GetProductById(productEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ProductDto>(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(productEntity.Category.CategoryName, result.CategoryName);
        Assert.Equal(productEntity.ProductDescription, result.ProductDescription);
        Assert.Equal(productEntity.Price, result.Price);
    }

    [Fact]
    public void GetAllProducts_Should_Find_And_Return_All_Products_InDB()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        var productEntity_1 = productService.CreateProduct(new ProductDto { Title = "Test Product 1", ProductDescription = "Test Description", Price = 1.0000M, CategoryName = "Test Category" });
        var productEntity_2 = productService.CreateProduct(new ProductDto { Title = "Test Product 2", ProductDescription = "Test Description", Price = 1.0000M, CategoryName = "Test Category" });

        // Act
        var result = productService.GetAllProducts();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        Assert.Equal(1, result.First().Id);
        Assert.Equal(2, result.Last().Id);
    }

    [Fact]
    public void UpdateProduct_Should_Update_Existing_Product_InDB_And_Return_Updated_Product()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var storeService = new StoreService(new StoreRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        var storeEntity = storeService.CreateStore("Test Store");
        var propertyEntity = propertyService.CreateProperty(new Property { Title = "Manufacturer", PropertyValue = "Test Manufacturer" });
        var productEntity = productService.CreateProduct(new ProductDto { 
            Title = "Test Product", 
            ProductDescription = "Test Description", 
            Price = 1.0000M, 
            CategoryName = "Test Category"
        });
        var productToUpdate = new ProductDto
        {
            Id = productEntity.Id,
            ProductDescription = "Updated Description",
            Price = 2.0000M,
            CategoryName = "Updated Category",
            Inventories = new List<Inventory> { new Inventory { ProductId = productEntity.Id, StoreId = storeEntity.Id, Amount = 10 } },
            Properties = new List<Property> { propertyEntity }
        };

        // Act
        var result = productService.UpdateProduct(productToUpdate);
        var existingInventory = inventoryService.GetInventory(productEntity.Id, storeEntity.Id);
        var existingProperty = propertyService.GetProperty(productToUpdate.Properties.First());

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Product>(result);
        Assert.Equal(productToUpdate.Id, result.Id);
        Assert.Equal(productToUpdate.CategoryName, result.Category.CategoryName);
        Assert.Equal(productToUpdate.ProductDescription, result.ProductDescription);
        Assert.Equal(productToUpdate.Price, result.Price);
        Assert.Equal(existingInventory, result.Inventories.First());
        Assert.Equal(existingProperty, result.Properties.First());
    }

    [Fact]
    public void DeleteProduct_Should_Delete_Existing_Product_FromDB_And_Return_True()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        var productEntity = productService.CreateProduct(new ProductDto
        {
            Title = "Test Product",
            ProductDescription = "Test Description",
            Price = 1.0000M,
            CategoryName = "Test Category"
        });

        // Act
        var result = productService.DeleteProduct(productEntity.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteProduc_Should_Not_Delete_Product_FromDB_If_Not_Existing_And_Return_False()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var inventoryService = new InventoryService(new InventoryRepository(_context));
        var propertyService = new PropertyService(new PropertyRepository(_context));
        var productService = new ProductService(productRepository, categoryService, inventoryService, propertyService);
        var productEntity = productService.CreateProduct(new ProductDto
        {
            Title = "Test Product",
            ProductDescription = "Test Description",
            Price = 1.0000M,
            CategoryName = "Test Category"
        });

        // Act
        var result = productService.DeleteProduct(2);

        //Assert
        Assert.False(result);
    }
}