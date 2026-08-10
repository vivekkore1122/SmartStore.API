namespace SmartStore.API.Models.Domain;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    // Foreign Key
    public int CategoryId { get; set; }

    // Navigation Property
    public Category Category { get; set; } = null!;

    // Foreign Key
    public int SupplierId { get; set; }

    // Navigation Property
    public Supplier Supplier { get; set; } = null!;
}