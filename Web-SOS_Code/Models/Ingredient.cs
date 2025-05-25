namespace Web_SOS_Code.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}