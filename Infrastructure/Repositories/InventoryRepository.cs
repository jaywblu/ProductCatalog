using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class InventoryRepository : Repo<Inventory, DataContext>
{
    public InventoryRepository(DataContext context) : base(context)
    {
    }
}