using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities.CustomerData;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Street { get; set; } = null!;

    [Required]
    [StringLength(6)]
    public string PostalCode { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Country { get; set; } = null!;
}
