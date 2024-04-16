using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class PropertyRepository : Repo<Property, DataContext>
{
    public PropertyRepository(DataContext context) : base(context)
    {
    }
}
