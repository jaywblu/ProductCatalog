using Infrastructure.Entities;
using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CategoryService(CategoryRepository categroyRepository)
{
    private readonly CategoryRepository _categoryRepository = categroyRepository;

    public Category CreateCategory(string categoryName)
    {
        var categoryEntity = new Category();
        try
        {
            categoryEntity = _categoryRepository.Get(x => x.CategoryName == categoryName);
            categoryEntity ??= _categoryRepository.Create(new Category { CategoryName = categoryName });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error: " + ex.Message);
        }
        
        return categoryEntity;
    }

    public Category GetCategoryByName(string categoryName)
    {
        var categoryEntity = _categoryRepository.Get(x => x.CategoryName == categoryName);
        return categoryEntity;
    }

    public Category GetCategoryById(int categoryId)
    {
        var categoryEntity = _categoryRepository.Get(x => x.Id == categoryId);
        return categoryEntity;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        var categories = _categoryRepository.GetAll();
        return categories;
    }

    public bool UpdateCategory(Category categoryEntity)
    {
        var existingCategory = _categoryRepository.Existing(x => x.CategoryName == categoryEntity.CategoryName);
        if (!existingCategory)
        {
            var updatedCategory = _categoryRepository.Update(categoryEntity, x => x.Id == categoryEntity.Id);
            return true;
        }
        return false;
    }

    public bool DeleteCategory(int id)
    {
        var result = _categoryRepository.Delete(x => x.Id == id);
        return result;
    }
}