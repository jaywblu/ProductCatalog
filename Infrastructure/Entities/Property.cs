using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Property
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string PropertyValue { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
