namespace ProductManagement.DTOs.Update;

public class UpdateSellerDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Avatarpath { get; set; }

    public string? Wallpaperpath { get; set; }

    public bool? Isvalidated { get; set; }
}