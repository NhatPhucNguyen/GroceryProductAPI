﻿using System;
using System.Collections.Generic;

namespace GroceryProductAPI.Models;

public partial class Product
{
    public string Upc { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string Brand { get; set; } = null!;

    public int Price { get; set; }

    public string NutrionInfo { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string Unit { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
