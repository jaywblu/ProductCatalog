using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class InventoryRepository_Tests
{
    private readonly DataContext _context = new DataContext(
        new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public void Create_Should_Add_One_Entity_ToDB_And_Return_Created_Entity()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = new Inventory { ProductId = 1, StoreId = 1, Amount = 1 };

        // Act
        var result = inventoryRepository.Create(inventoryEntity);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Inventory>(result);
        Assert.Equal(1, result.ProductId);
        Assert.Equal(1, result.StoreId);
        Assert.Equal(1, result.Amount);
    }

    [Fact]
    public void GetAll_Should_Return_All_Entities()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });

        // Act
        var result = inventoryRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Inventory>>(result);
        Assert.Equal(inventoryEntity, result.First());
    }

    [Fact]
    public void GetAllWhere_Should_Return_All_Entities_Where_Expression_Is_True()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity_1 = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });
        var inventoryEntity_2 = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 2, Amount = 2 });

        // Act
        var result = inventoryRepository.GetAllWhere(e => e.ProductId == 1);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Inventory>>(result);
        Assert.Equal(1, result.First().StoreId);
        Assert.Equal(2, result.Last().StoreId);
    }

    [Fact]
    public void Get_Should_Return_First_Entity_Where_Expression_Is_True()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity_1 = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });
        var inventoryEntity_2 = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 2, Amount = 2 });
        var inventoryEntity_3 = inventoryRepository.Create(new Inventory { ProductId = 2, StoreId = 2, Amount = 3 });

        // Act
        var result = inventoryRepository.Get(e => e.StoreId == 2);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Inventory>(result);
        Assert.Equal(2, result.Amount);
    }

    [Fact]
    public void Update_Should_Update_Existing_Entity_InDB_And_Return_Updated_Entity()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });
        var entityToUpdate = inventoryRepository.Get(e => e.Equals(inventoryEntity));
        entityToUpdate.Amount = 2;

        // Act
        var result = inventoryRepository.Update(entityToUpdate, e => e.Equals(inventoryEntity));

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Inventory>(result);
        Assert.Equal(entityToUpdate, result);
    }

    [Fact]
    public void Delete_Should_Delete_Existing_Entity_From_DB_And_Return_True()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });

        // Act
        var result = inventoryRepository.Delete(e => e.Equals(inventoryEntity));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_Should_Not_Delete_Existing_Entity_From_DB_If_Provided_Entity_Does_Not_Match_And_Return_False()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });

        // Act
        var result = inventoryRepository.Delete(e => e.ProductId == 2 && e.StoreId == 2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Existing_Should_Return_True_If_Expression_Finds_Existing_Entity_InDB()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });

        // Act
        var result = inventoryRepository.Existing(e => e.Equals(inventoryEntity));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Existing_Should_Return_False_If_Expression_Does_Not_Find_Existing_Entity_InDB()
    {
        // Arrange
        var inventoryRepository = new InventoryRepository(_context);
        var inventoryEntity = inventoryRepository.Create(new Inventory { ProductId = 1, StoreId = 1, Amount = 1 });
        var tmpEntity = new Inventory { ProductId = 2, StoreId = 2, Amount = 2 };

        // Act
        var result = inventoryRepository.Existing(e => e.Equals(tmpEntity));

        // Assert
        Assert.False(result);
    }
}