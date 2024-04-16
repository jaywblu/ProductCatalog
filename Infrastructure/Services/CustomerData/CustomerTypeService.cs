using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories.CustomerData;

namespace Infrastructure.Services.CustomerData;

public class CustomerTypeService(CustomerTypeRepository customerTypeRepo)
{
    private readonly CustomerTypeRepository _customerTypeRepo = customerTypeRepo;

    public CustomerTypeEntity CreateCustomerType(string name)
    {
        var customerTypeEntity = _customerTypeRepo.Get(x => x.Name == name);

        customerTypeEntity ??= _customerTypeRepo.Create(new CustomerTypeEntity { Name = name });

        return customerTypeEntity;
    }

    public CustomerTypeEntity GetCustomerTypeById(int id)
    {
        var customerTypeEntity = _customerTypeRepo.Get(x => x.Id == id);

        return customerTypeEntity;
    }

    public CustomerTypeEntity GetCustomerTypeByName(string name)
    {
        var customerTypeEntity = _customerTypeRepo.Get(x => x.Name == name);

        return customerTypeEntity;
    }

    public IEnumerable<CustomerTypeEntity> GetAllCustomerTypes()
    {
        return _customerTypeRepo.GetAll();
    }

    public CustomerTypeEntity UpdateCustomerType(CustomerTypeEntity customerTypeEntity)
    {
        var updatedEntity = _customerTypeRepo.Update(customerTypeEntity, x => x.Id == customerTypeEntity.Id);

        return updatedEntity;
    }

    public bool DeleteCustomerTypeByName(string name)
    {
        var result = _customerTypeRepo.Delete(x => x.Name == name);
        return result;
    }

    public bool DeleteCustomerTypeById(int id)
    {
        var result = _customerTypeRepo.Delete(x => x.Id == id);
        return result;
    }
}
