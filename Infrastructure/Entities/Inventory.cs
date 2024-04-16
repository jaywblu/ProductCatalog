using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Inventory
{
    public int StoreId { get; set; }

    public int ProductId { get; set; }

    public int Amount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
