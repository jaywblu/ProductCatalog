using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Store
{
    public int Id { get; set; }

    public string StoreName { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
