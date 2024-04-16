using Infrastructure.Contexts;
using Infrastructure.Entities.CustomerData;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.CustomerData;

public class ActiveCampaignRepository : Repo<ActiveCampaignEntity, CustomerDataContext>
{
    public ActiveCampaignRepository(CustomerDataContext context) : base(context)
    {
    }
}
