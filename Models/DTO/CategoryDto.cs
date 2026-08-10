namespace SmartStore.API.Models.DTO;

public class CategoryDto
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}