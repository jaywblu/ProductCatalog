using Infrastructure.Contexts;
using Infrastructure.Entities.CustomerData;

namespace Infrastructure.Repositories.CustomerData;

public class CustomerTypeRepository : Repo<CustomerTypeEntity, CustomerDataContext>
{
    public CustomerTypeRepository(CustomerDataContext context) : base(context)
    {
    }
}