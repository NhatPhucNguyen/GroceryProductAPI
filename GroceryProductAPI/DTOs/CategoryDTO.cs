﻿namespace GroceryProductAPI.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
