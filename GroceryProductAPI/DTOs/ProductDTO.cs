namespace GroceryProductAPI.DTOs
{
    public class ProductDTO
    {
        public string Upc { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public int Price { get; set; }

        public string NutrionInfo { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string Unit { get; set; } = null!;

        public int Quantity { get; set; }
        public List<string> Ingredients {  get; set; } = null!;
    }
}
