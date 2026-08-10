namespace SmartStore.API.Models.Domain;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Supplier { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}