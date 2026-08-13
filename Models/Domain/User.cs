namespace SmartStore.API.Models.Domain;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}