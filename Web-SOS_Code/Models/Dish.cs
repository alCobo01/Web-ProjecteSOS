namespace Web_SOS_Code.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public double Price { get; set; }
        public List<string> IngredientsName { get; set; } = new List<string>();
    }
}