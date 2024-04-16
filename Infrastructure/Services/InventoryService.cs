using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class InventoryService(InventoryRepository inventoryRepository)
{
    private readonly InventoryRepository _inventoryRepository = inventoryRepository;

    public Inventory CreateInventory(Inventory inventory)
    {
        var inventoryEntity = _inventoryRepository.Get(x => x.StoreId == inventory.StoreId && x.ProductId == inventory.ProductId);
        inventoryEntity ??= _inventoryRepository.Create(inventory);

        return inventoryEntity;
    }

    public Inventory GetInventory(int productId, int storeId) {
        return _inventoryRepository.Get(x => x.StoreId == storeId && x.ProductId == productId);
    }

    public IEnumerable<Inventory> GetInventoriesByStore(int storeId)
    {
        var results = _inventoryRepository.GetAllWhere(x => x.StoreId == storeId);

        return results;
    }

    public IEnumerable<Inventory> GetInventoriesByProduct(int productId)
    {
        var results = _inventoryRepository.GetAllWhere(x => x.ProductId == productId);

        return results;
    }

    public IEnumerable<Inventory> GetAllInventories()
    {
        return _inventoryRepository.GetAll();
    }

    public Inventory UpdateInventory(Inventory inventoryEntity)
    {
        var updatedInventory = _inventoryRepository.Update(inventoryEntity, x => x.StoreId == inventoryEntity.StoreId && x.ProductId == inventoryEntity.ProductId);
        return updatedInventory;
    }

    public bool DeleteInventoryy(int storeId, int productId)
    {
        var result = _inventoryRepository.Delete(x => x.StoreId == storeId && x.ProductId == productId);
        return result;
    }
}