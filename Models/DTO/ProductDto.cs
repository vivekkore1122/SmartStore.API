namespace SmartStore.API.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string Category { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public string Supplier { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}