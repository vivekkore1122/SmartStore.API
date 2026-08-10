namespace SmartStore.API.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        // Navigation Property
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
