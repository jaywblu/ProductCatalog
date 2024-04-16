using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.CustomerData;

public class CustomerProfileEntity
{
    [Key]
    [ForeignKey(nameof(CustomerEntity))]
    public Guid CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(AddressEntity))]
    public int AddressId { get; set; }

    public virtual CustomerEntity Customer { get; set; } = null!;
    public virtual AddressEntity Address { get; set; } = null!;
}