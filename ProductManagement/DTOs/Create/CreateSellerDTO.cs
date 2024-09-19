namespace ProductManagement.DTOs.Create;

public class CreateSellerDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Avatarpath { get; set; }

    public string? Wallpaperpath { get; set; }
}