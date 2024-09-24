using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechnoBit.Models;

public partial class TechnobitpublicContext : DbContext
{
    public TechnobitpublicContext()
    {
    }

    public TechnobitpublicContext(DbContextOptions<TechnobitpublicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-nameless-boat-a2e1c4ib.eu-central-1.aws.neon.tech;Port=5432;Username=bites;Password=OZXjeuC6hlW4;Database=technobitpublic;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
