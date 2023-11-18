namespace GroceryProductAPI.DTOs
{
    public class ProductForUpdateDTO
    {
        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }

        public string Brand { get; set; } = null!;

        public decimal Price { get; set; }

        public string NutritionInfo { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string Unit { get; set; } = null!;

        public int Quantity { get; set; }
        public List<string> IngredientsList { get; set; } = null!;
    }
}
