namespace ProductManagement.DTOs.Base;

public class BaseSellerDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Avatarpath { get; set; }

    public string? Wallpaperpath { get; set; }
}