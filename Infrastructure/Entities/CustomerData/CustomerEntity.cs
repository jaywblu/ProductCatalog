using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.CustomerData;

public class CustomerEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Password { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(CustomerTypeEntity))]
    public int CustomerTypeId { get; set; }

    public virtual CustomerTypeEntity CustomerType { get; set; } = null!;
    public virtual CustomerProfileEntity CustomerProfile { get; set; } = null!;
}
