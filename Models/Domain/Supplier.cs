namespace SmartStore.API.Models.Domain;

public class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}