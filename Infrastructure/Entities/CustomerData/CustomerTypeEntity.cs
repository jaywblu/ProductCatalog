using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities.CustomerData;

public class CustomerTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<CustomerEntity> Customers { get; set; } = new HashSet<CustomerEntity>();
    public ICollection<ActiveCampaignEntity> ActiveCampaigns { get; set; } = null!;
}