using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GroceryProductAPI.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    [Ignore]
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
