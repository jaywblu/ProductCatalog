using Infrastructure.Contexts;
using Infrastructure.Entities.CustomerData;

namespace Infrastructure.Repositories.CustomerData;

public class CustomerRepository : Repo<CustomerEntity, CustomerDataContext>
{
    public CustomerRepository(CustomerDataContext context) : base(context)
    {
    }
}