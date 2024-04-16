using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CategoryRepository : Repo<Category, DataContext>
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }
}