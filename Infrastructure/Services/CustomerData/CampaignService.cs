using Infrastructure.Entities.CustomerData;
using Infrastructure.Repositories.CustomerData;

namespace Infrastructure.Services.CustomerData;

public class CampaignService(CampaignRepository campaignRepo)
{
    private readonly CampaignRepository _campaignRepo = campaignRepo;

    public CampaignEntity CreateCampaign(CampaignEntity campaignEntity)
    {
        return _campaignRepo.Create(campaignEntity);
    }

    public CampaignEntity GetCampaignById(int id)
    {
        var campaignEntity = _campaignRepo.Get(x => x.Id == id);

        return campaignEntity;
    }

    public IEnumerable<CampaignEntity> GetAllCampaigns()
    {
        return _campaignRepo.GetAll();
    }

    public CampaignEntity UpdateCampaign(CampaignEntity campaignEntity)
    {
        var updatedEntity = _campaignRepo.Update(campaignEntity, x => x.Id == campaignEntity.Id);

        return updatedEntity;
    }

    public bool DeleteCampaign(int id)
    {
        var result = _campaignRepo.Delete(x => x.Id == id);
        return result;
    }
}