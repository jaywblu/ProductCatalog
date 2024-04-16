using Infrastructure.Contexts;
using Infrastructure.Entities.CustomerData;

namespace Infrastructure.Repositories.CustomerData;

public class CustomerProfileRepository : Repo<CustomerProfileEntity, CustomerDataContext>
{
    public CustomerProfileRepository(CustomerDataContext context) : base(context)
    {
    }
}
