using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories.CustomerData;

namespace Infrastructure.Services.CustomerData;

public class ActiveCampaignService(ActiveCampaignRepository activeCampaignRepo)
{
    private readonly ActiveCampaignRepository _activeCampaignRepo = activeCampaignRepo;

    public ActiveCampaignEntity CreateActiveCampaign(int campaignId, int customerTypeId)
    {
        var activeCampaignEntity = _activeCampaignRepo.Get(x => x.CampaignId == campaignId && x.CustomerTypeId == customerTypeId);

        activeCampaignEntity ??= _activeCampaignRepo.Create(new ActiveCampaignEntity { CampaignId = campaignId, CustomerTypeId = customerTypeId });

        return activeCampaignEntity;
    }

    public IEnumerable<ActiveCampaignEntity> GetActiveCampaignsById(int campaignId)
    {
        var results = _activeCampaignRepo.GetAllWhere(x => x.CampaignId == campaignId);

        return results;
    }

    public IEnumerable<ActiveCampaignEntity> GetActiveCampaignsByCustomerType(int customerTypeId)
    {
        var results = _activeCampaignRepo.GetAllWhere(x => x.CustomerTypeId == customerTypeId);

        return results;
    }

    public IEnumerable<ActiveCampaignEntity> GetAllActiveCampaigns()
    {
        return _activeCampaignRepo.GetAll();
    }

    //public ActiveCampaignEntity UpdateActiveCampaign(ActiveCampaignEntity activeCampaignEntity)
    //{
        
    //}

    public bool DeleteActiveCampaign(int campaignId, int customerTypeId)
    {
        var result = _activeCampaignRepo.Delete(x => x.CampaignId == campaignId && x.CustomerTypeId == customerTypeId);
        return result;
    }
}