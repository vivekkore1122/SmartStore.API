public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}