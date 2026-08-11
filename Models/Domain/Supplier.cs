namespace SmartStore.API.Models.Domain;

public class Supplier
{
    public virtual int Id { get; set; }

    public virtual string SupplierName { get; set; } = string.Empty;

    public virtual string? Phone { get; set; }

    public virtual string? Email { get; set; }
}