namespace SmartStore.API.Models.Domain;

public class Product
{
    public virtual int Id { get; set; }

    public virtual string Name { get; set; } = string.Empty;

    public virtual string ProductCode { get; set; } = string.Empty;

    public virtual decimal Price { get; set; }

    public virtual int Quantity { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}