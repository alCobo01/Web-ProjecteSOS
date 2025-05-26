namespace Web_SOS_Code.Models.Menu
{
    public class MenuDish
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public double Price { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}