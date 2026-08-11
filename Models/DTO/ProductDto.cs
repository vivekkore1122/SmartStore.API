namespace SmartStore.API.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductCode { get; set; }
        public int CategoryId { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int SupplierId { get; set; }
    }
}
