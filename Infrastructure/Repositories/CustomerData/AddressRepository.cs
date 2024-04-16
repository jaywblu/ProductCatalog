using Infrastructure.Contexts;
using Infrastructure.Entities.CustomerData;

namespace Infrastructure.Repositories.CustomerData;

public class AddressRepository : Repo<AddressEntity, CustomerDataContext>
{
    public AddressRepository(CustomerDataContext context) : base(context)
    {
    }
}
