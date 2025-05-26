namespace Web_SOS_Code.Models.Menu
{
    public class MenuIngredient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}