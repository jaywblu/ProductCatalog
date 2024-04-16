using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class CategoryRepository_Tests
{
    private readonly DataContext _context = new DataContext(
        new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public void Create_Should_Add_One_Entity_ToDB_And_Return_Created_Entity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new Category { CategoryName = "Test Category" };

        // Act
        var result = categoryRepository.Create(categoryEntity);

        // Assert
        Assert.NotNull( result );
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);
    }

    [Fact]
    public void GetAll_Should_Return_All_Entities()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity_1 = categoryRepository.Create(new Category { CategoryName = "Test Category 1" });
        var categoryEntity_2 = categoryRepository.Create(new Category { CategoryName = "Test Category 2" });

        // Act
        var result = categoryRepository.GetAll();

        // Assert
        Assert.NotNull( result );
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.Equal(1, result.First().Id);
        Assert.Equal(2, result.Last().Id);
    }

    [Fact]
    public void GetAllWhere_Should_Return_All_Entities_Where_Expression_Is_True()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity_1 = categoryRepository.Create(new Category { CategoryName = "Test Category 1" });
        var categoryEntity_2 = categoryRepository.Create(new Category { CategoryName = "Test Category 2" });

        // Act
        var result = categoryRepository.GetAllWhere(e => e.CategoryName == "Test Category 2");

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.Equal(2, result.First().Id);
    }

    [Fact]
    public void Get_Should_Return_First_Entity_Where_Expression_Is_True()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity_1 = categoryRepository.Create(new Category { CategoryName = "Test Category 1" });
        var categoryEntity_2 = categoryRepository.Create(new Category { CategoryName = "Test Category 2" });

        // Act
        var result = categoryRepository.Get(e => e.CategoryName == "Test Category 2");

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(2, result.Id);
    }

    [Fact]
    public void Update_Should_Update_Existing_Entity_InDB_And_Return_Updated_Entity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = categoryRepository.Create(new Category { CategoryName = "Test Category" });
        var entityToUpdate = categoryRepository.Get(e => e.Id == 1);
        entityToUpdate.CategoryName = "Updated Category Name";

        // Act
        var result = categoryRepository.Update(entityToUpdate, e => e.Equals(categoryEntity));

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(entityToUpdate.CategoryName, result.CategoryName);
    }

    [Fact]
    public void Delete_Should_Delete_Existing_Entity_From_DB_And_Return_True()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = categoryRepository.Create(new Category { CategoryName = "Test Category" });

        // Act
        var result = categoryRepository.Delete(e => e.Id == categoryEntity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_Should_Not_Delete_Existing_Entity_From_DB_If_Provided_Entity_Does_Not_Match_And_Return_False()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = categoryRepository.Create(new Category { CategoryName = "Test Category" });

        // Act
        var result = categoryRepository.Delete(e => e.Id == 2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Existing_Should_Return_True_If_Expression_Finds_Existing_Entity_InDB()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = categoryRepository.Create(new Category { CategoryName = "Test Category" });

        // Act
        var result = categoryRepository.Existing(e => e.Id == 1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Existing_Should_Return_False_If_Expression_Does_Not_Find_Existing_Entity_InDB()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = categoryRepository.Create(new Category { CategoryName = "Test Category" });

        // Act
        var result = categoryRepository.Existing(e => e.Id == 2);

        // Assert
        Assert.False(result);
    }
}
