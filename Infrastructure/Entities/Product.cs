using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
