using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Tests.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

public class CategoryService_Tests
{
    private readonly DataContext _context = new DataContext(
       new DbContextOptionsBuilder<DataContext>()
       .UseInMemoryDatabase($"{Guid.NewGuid()}")
       .Options);

    [Fact]
    public void CreateCategory_Should_Add_One_Category_ToDB_If_CategoryName_Is_Unique_And_Return_New_Category()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test Category";

        // Act
        var result = categoryService.CreateCategory(categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(categoryName, result.CategoryName);
    }

    [Fact]
    public void CreateCategory_Should_Not_Add_Category_ToDB_If_CategoryName_Already_Exists_And_Return_Existing_Category()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test Category";
        var existingCategory = categoryService.CreateCategory(categoryName);

        // Act
        var result = categoryService.CreateCategory(categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(existingCategory.Id, result.Id);
        Assert.Equal(categoryName, result.CategoryName);
    }

    [Fact]
    public void GetCategoryByName_Should_Find_And_Return_Existing_Category_By_Provided_Name()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test Category";
        var categoryEntity = categoryService.CreateCategory(categoryName);

        // Act
        var result = categoryService.GetCategoryByName(categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(categoryEntity.Id, result.Id);
        Assert.Equal(categoryName, result.CategoryName);
    }

    [Fact]
    public void GetCategoryById_Should_Find_And_Return_Existing_Category_By_Provided_Id()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test Category";
        var categoryEntity = categoryService.CreateCategory(categoryName);

        // Act
        var result = categoryService.GetCategoryById(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Category>(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(categoryName, result.CategoryName);
    }

    [Fact]
    public void GetAllCategories_Should_Return_All_Categories()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var categoryEntity_1 = categoryService.CreateCategory("Test Category 1");
        var categoryEntity_2 = categoryService.CreateCategory("Test Category 2");

        // Act
        var result = categoryService.GetAllCategories();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.Equal(categoryEntity_1, result.First());
        Assert.Equal(categoryEntity_2, result.Last());
    }

    [Fact]
    public void UpdateCategory_Should_Update_CategoryName_Of_Existing_Category_If_Not_Already_Existing_InDB_And_Return_True()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var categoryEntity = categoryService.CreateCategory("Test Category 1");
        categoryEntity.CategoryName = "Updated Category Name";

        // Act
        var result = categoryService.UpdateCategory(categoryEntity);
        categoryEntity = categoryService.GetCategoryById(categoryEntity.Id);

        //Assert
        Assert.True(result);
        Assert.Equal("Updated Category Name", categoryEntity.CategoryName);
    }

    [Fact]
    public void UpdateCategory_Should_Not_Update_CategoryName_Of_Existing_Category_If_Already_Existing_InDB_And_Return_False()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var categoryEntity_1 = categoryService.CreateCategory("Test Category 1");
        var categoryEntity_2 = categoryService.CreateCategory("Test Category 2");
        var categoryToUpdate = new Category { Id = 1, CategoryName = "Test Category 2" };

        // Act
        var result = categoryService.UpdateCategory(categoryToUpdate);
        var categoryEntity = categoryService.GetCategoryById(categoryEntity_1.Id);

        //Assert
        Assert.False(result);
        Assert.Equal(categoryEntity.CategoryName, categoryEntity_1.CategoryName);
    }

    [Fact]
    public void DeleteCategory_Should_Delete_Existing_Category_FromDB_And_Return_True()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var categoryEntity = categoryService.CreateCategory("Test Category 1");

        // Act
        var result = categoryService.DeleteCategory(1);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteCategory_Should_Not_Delete_Category_FromDB_If_Not_Existing_And_Return_False()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var categoryEntity = categoryService.CreateCategory("Test Category 1");

        // Act
        var result = categoryService.DeleteCategory(2);

        //Assert
        Assert.False(result);
    }
}
