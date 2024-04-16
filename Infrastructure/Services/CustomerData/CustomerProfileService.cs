using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories.CustomerData;

namespace Infrastructure.Services.CustomerData;

public class CustomerProfileService(CustomerProfileRepository customerProfileRepo)
{
    private readonly CustomerProfileRepository _customerProfileRepo = customerProfileRepo;

    public CustomerProfileEntity CreateCustomerProfile(CustomerProfileEntity customerProfileEntity)
    {
        var existingCustomerProfile = _customerProfileRepo.Get(x => x.CustomerId == customerProfileEntity.CustomerId);

        if (existingCustomerProfile == null)
        {
            _customerProfileRepo.Create(customerProfileEntity);
            return customerProfileEntity;
        }

        return existingCustomerProfile;
    }

    public CustomerProfileEntity GetCustomerProfileById(Guid id)
    {
        var customerProfileEntity = _customerProfileRepo.Get(x => x.CustomerId == id);

        return customerProfileEntity;
    }

    public IEnumerable<CustomerProfileEntity> GetAllCustomerProfiles()
    {
        return _customerProfileRepo.GetAll();
    }

    public CustomerProfileEntity UpdateCustomerProfile(CustomerProfileEntity customerProfileEntity)
    {
        var updatedEntity = _customerProfileRepo.Update(customerProfileEntity, x => x.CustomerId == customerProfileEntity.CustomerId);

        return updatedEntity;
    }

    public bool DeleteCustomerProfile(Guid id)
    {
        var result = _customerProfileRepo.Delete(x => x.CustomerId == id);
        return result;
    }
}