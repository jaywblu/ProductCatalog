using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities.CustomerData;

public class CampaignEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = null!;

    public ICollection<ActiveCampaignEntity> ActiveCampaigns { get; set; } = null!;
}
