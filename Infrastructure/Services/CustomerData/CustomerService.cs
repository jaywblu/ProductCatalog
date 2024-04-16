using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories.CustomerData;

namespace Infrastructure.Services.CustomerData;

public class CustomerService(CustomerRepository customerRepo)
{
    private readonly CustomerRepository _customerRepo = customerRepo;

    public CustomerEntity CreateCustomer(CustomerEntity customerEntity)
    {
        var existingCustomer = _customerRepo.Get(x => x.Email == customerEntity.Email);

        if (existingCustomer == null)
        {
            _customerRepo.Create(customerEntity);
            return customerEntity;
        }

        return existingCustomer;
    }

    public CustomerEntity GetCustomerByEmail(string email)
    {
        var customerEntity = _customerRepo.Get(x => x.Email == email);

        return customerEntity;
    }

    public CustomerEntity GetCustomerById(Guid id)
    {
        var customerEntity = _customerRepo.Get(x => x.Id == id);

        return customerEntity;
    }

    public IEnumerable<CustomerEntity> GetAllCustomers()
    {
        return _customerRepo.GetAll();
    }

    public CustomerEntity UpdateCustomer(CustomerEntity customerEntity)
    {
        var updatedEntity = _customerRepo.Update(customerEntity, x => x.Email == customerEntity.Email);

        return updatedEntity;
    }

    public bool DeleteCustomer(Guid id)
    {
        var result = _customerRepo.Delete(x => x.Id == id);
        return result;
    }
}
