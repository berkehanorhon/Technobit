namespace ProductManagement.DTOs.Read;

public class SellerDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Avatarpath { get; set; }

    public string? Wallpaperpath { get; set; }

    public bool? Isvalidated { get; set; }
}