using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities.CustomerData;

//[PrimaryKey(nameof(CampaignEntity), nameof(CustomerTypeEntity))]
public class ActiveCampaignEntity
{
    [Required]
    public int CampaignId { get; set; }
    public CampaignEntity Campaign { get; set; } = null!;

    [Required]
    public int CustomerTypeId { get; set; }
    public CustomerTypeEntity CustomerType { get; set; } = null!;
}