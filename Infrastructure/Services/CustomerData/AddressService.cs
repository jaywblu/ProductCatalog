using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories.CustomerData;

namespace Infrastructure.Services.CustomerData;

public class AddressService(AddressRepository addressRepo)
{
    private readonly AddressRepository _addressRepo = addressRepo;

    public AddressEntity CreateAddress(AddressEntity addressEntity)
    {
        return _addressRepo.Create(addressEntity);
    }

    public AddressEntity GetAddressById(int id)
    {
        var addressEntity = _addressRepo.Get(x => x.Id == id);

        return addressEntity;
    }

    public IEnumerable<AddressEntity> GetAllAddresses()
    {
        return _addressRepo.GetAll();
    }

    public AddressEntity UpdateAddress(AddressEntity addressEntity)
    {
        var updatedEntity = _addressRepo.Update(addressEntity, x => x.Id == addressEntity.Id);

        return updatedEntity;
    }

    public bool DeleteAddress(int id)
    {
        var result = _addressRepo.Delete(x => x.Id == id);
        return result;
    }
}