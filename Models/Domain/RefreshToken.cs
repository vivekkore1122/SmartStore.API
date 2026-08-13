namespace SmartStore.API.Models.Domain;

public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}