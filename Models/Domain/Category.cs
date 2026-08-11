namespace SmartStore.API.Models.Domain;

public class Category
{
    public virtual int Id { get; set; }

    public virtual string CategoryName { get; set; } = string.Empty;

    public virtual string? Description { get; set; }
}