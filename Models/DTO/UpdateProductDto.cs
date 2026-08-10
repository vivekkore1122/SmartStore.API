namespace SmartStore.API.Models.DTO
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

    }
}
