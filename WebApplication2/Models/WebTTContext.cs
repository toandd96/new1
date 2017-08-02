using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public partial class WebTTContext : IdentityDbContext<ApplicationUser,IdentityRole<string>,string>
    {


        public virtual DbSet<Chuyenmuc> Chuyenmuc { get; set; }
        public virtual DbSet<Tinnhanh> Tinnhanh { get; set; }
        public virtual DbSet<Tintuc> Tintuc { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-EKNGHDH\SQLEXPRESS;Database=WebTT;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chuyenmuc>(entity =>
            {
                entity.HasKey(e => e.Machuyenmuc)
                    .HasName("PK_chuyenmuc");

                entity.ToTable("chuyenmuc");

                entity.Property(e => e.Machuyenmuc).HasColumnName("machuyenmuc");

                //entity.Property(e => e.Sobaiviet).HasColumnName("sobaiviet");

                entity.Property(e => e.Tenchuyenmuc)
                    .HasColumnName("tenchuyenmuc")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tinnhanh>(entity =>
            {
                entity.HasKey(e => e.Matinnhanh)
                    .HasName("PK_tinnhanh");

                entity.ToTable("tinnhanh");

                entity.Property(e => e.Matinnhanh).HasColumnName("matinnhanh");

                entity.Property(e => e.Machuyenmuc).HasColumnName("machuyenmuc");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.HasOne(d => d.MachuyenmucNavigation)
                    .WithMany(p => p.Tinnhanh)
                    .HasForeignKey(d => d.Machuyenmuc)
                    .HasConstraintName("FK_tinnhanh_chuyenmuc");
            });

            modelBuilder.Entity<Tintuc>(entity =>
            {
                entity.HasKey(e => e.Matintuc)
                    .HasName("PK_tintuc");

                entity.ToTable("tintuc");

                entity.Property(e => e.Matintuc).HasColumnName("matintuc");

                entity.Property(e => e.Anh)
                    .HasColumnName("anh")
                    .HasMaxLength(500);

                entity.Property(e => e.Machuyenmuc).HasColumnName("machuyenmuc");

                entity.Property(e => e.Ngaydang)
                    .HasColumnName("ngaydang")
                    .HasColumnType("datetime");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Tacgia)
                    .HasColumnName("tacgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Tieude)
                    .HasColumnName("tieude")
                    .HasMaxLength(500);

                entity.Property(e => e.Tieudecon)
                    .HasColumnName("tieudecon")
                    .HasMaxLength(500);

                entity.HasOne(d => d.MachuyenmucNavigation)
                    .WithMany(p => p.Tintuc)
                    .HasForeignKey(d => d.Machuyenmuc)
                    .HasConstraintName("FK_tintuc_chuyenmuc");
            });
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<ApplicationUser>(e => e.ToTable("Users").HasKey(x => x.Id));

            modelBuilder.Entity<IdentityRole<string>>(e => e.ToTable("Roles").HasKey(x => x.Id));

            modelBuilder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaim").HasKey(x => x.Id));

            modelBuilder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles").HasKey(x => x.RoleId));

            modelBuilder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogin").HasKey(x => x.UserId));

            modelBuilder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims").HasKey(x => x.Id));

            modelBuilder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens").HasKey(x => x.UserId));
        }
    }
}