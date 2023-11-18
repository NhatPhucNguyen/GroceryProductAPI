using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GroceryProductAPI.Models;

public partial class Product
{
    public string Upc { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string Brand { get; set; } = null!;

    public decimal Price { get; set; }

    public string NutritionInfo { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string Unit { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    [Ignore]
    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;
    [Ignore]
    [JsonIgnore]
    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
