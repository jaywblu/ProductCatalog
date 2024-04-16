using Infrastructure.Contexts;
using Infrastructure.Entities.CustomerData;

namespace Infrastructure.Repositories.CustomerData;

public class CampaignRepository : Repo<CampaignEntity, CustomerDataContext>
{
    public CampaignRepository(CustomerDataContext context) : base(context)
    {
    }
}
