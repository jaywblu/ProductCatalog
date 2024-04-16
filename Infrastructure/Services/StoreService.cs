using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class StoreService(StoreRepository storeRepository)
{
    private readonly StoreRepository _storeRepository = storeRepository;

    public Store CreateStore(string storeName)
    {
        var storeEntity = _storeRepository.Get(x => x.StoreName == storeName);
        storeEntity ??= _storeRepository.Create(new Store { StoreName = storeName });

        return storeEntity;
    }

    public Store GetStoreByName(string storeName)
    {
        return _storeRepository.Get(x => x.StoreName == storeName);
    }

    public Store GetStoreById(int id)
    {
        return _storeRepository.Get(x => x.Id == id);
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _storeRepository.GetAll();
    }

    public Store UpdateCategory(Store storeEntity)
    {
        var updatedStore = _storeRepository.Update(storeEntity, x => x.Id == storeEntity.Id);
        return updatedStore;
    }

    public bool DeleteStore(int id)
    {
        var result = _storeRepository.Delete(x => x.Id == id);
        return result;
    }
}