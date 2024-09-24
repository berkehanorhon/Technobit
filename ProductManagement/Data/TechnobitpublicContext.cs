using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data;

public partial class TechnobitpublicContext : DbContext
{
    public TechnobitpublicContext()
    {
    }

    public TechnobitpublicContext(DbContextOptions<TechnobitpublicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Dummy> Dummies { get; set; }
    
    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productdetailtag> Productdetailtags { get; set; }

    public virtual DbSet<Productimage> Productimages { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Sellerproduct> Sellerproducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-nameless-boat-a2e1c4ib.eu-central-1.aws.neon.tech;Port=5432;Username=bites;Password=OZXjeuC6hlW4;Database=technobitpublic;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Subcategoryid).HasColumnName("subcategoryid");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.InverseSubcategory)
                .HasForeignKey(d => d.Subcategoryid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("category_subcategoryid_fkey");
        });

        modelBuilder.Entity<Dummy>(entity =>
        {
            entity.ToTable("Dummy");

            entity.Property(e => e.Int1).HasColumnName("_int1");
            entity.Property(e => e.Int2).HasColumnName("_int2");
            entity.Property(e => e.Int4).HasColumnName("_int4");
            entity.Property(e => e.Str1).HasColumnName("_str1");
            entity.Property(e => e.Str2).HasColumnName("_str2");
            entity.Property(e => e.Str3).HasColumnName("_str3");
        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("product_categoryid_fkey");
        });

        modelBuilder.Entity<Productdetailtag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productdetailtag_pkey");

            entity.ToTable("productdetailtag");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Product).WithMany(p => p.Productdetailtags)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("productdetailtag_productid_fkey");
        });

        modelBuilder.Entity<Productimage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productimage_pkey");

            entity.ToTable("productimage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("imagepath");
            entity.Property(e => e.Productid).HasColumnName("productid");

            entity.HasOne(d => d.Product).WithMany(p => p.Productimages)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("productimage_productid_fkey");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("seller_pkey");

            entity.ToTable("seller");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Avatarpath)
                .HasMaxLength(255)
                .HasColumnName("avatarpath");
            entity.Property(e => e.Isvalidated)
                .HasDefaultValue(false)
                .HasColumnName("isvalidated");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Wallpaperpath)
                .HasMaxLength(255)
                .HasColumnName("wallpaperpath");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Seller)
                .HasForeignKey<Seller>(d => d.Id)
                .HasConstraintName("seller_id_fkey");
        });

        modelBuilder.Entity<Sellerproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sellerproducts_pkey");

            entity.ToTable("sellerproducts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Sellerid).HasColumnName("sellerid");
            entity.Property(e => e.Stockquantity).HasColumnName("stockquantity");

            entity.HasOne(d => d.Product).WithMany(p => p.Sellerproducts)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("sellerproducts_productid_fkey");

            entity.HasOne(d => d.Seller).WithMany(p => p.Sellerproducts)
                .HasForeignKey(d => d.Sellerid)
                .HasConstraintName("sellerproducts_sellerid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsAdmin)
                .HasDefaultValue(false)
                .HasColumnName("isAdmin");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.RefreshToken).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
