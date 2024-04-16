using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class StoreRepository : Repo<Store, DataContext>
{
    public StoreRepository(DataContext context) : base(context)
    {
    }
}