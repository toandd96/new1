using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2.Data
{
    public partial class WebTTContext : DbContext
    {
        public virtual DbSet<Chuyenmuc> Chuyenmuc { get; set; }
        public virtual DbSet<Credential> Credential { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleClaims> RoleClaims { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tinnhanh> Tinnhanh { get; set; }
        public virtual DbSet<Tintuc> Tintuc { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UserTokens> UserTokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-EKNGHDH ;Database=WebTT ;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chuyenmuc>(entity =>
            {
                entity.HasKey(e => e.Machuyenmuc)
                    .HasName("PK_chuyenmuc");

                entity.ToTable("chuyenmuc");

                entity.Property(e => e.Machuyenmuc).HasColumnName("machuyenmuc");

                entity.Property(e => e.Tenchuyenmuc)
                    .IsRequired()
                    .HasColumnName("tenchuyenmuc")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => new { e.UserGroupId, e.RoleId })
                    .HasName("PK_Credential");

                entity.Property(e => e.UserGroupId)
                    .HasColumnName("UserGroupID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Credential)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Credential_Role");

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.Credential)
                    .HasForeignKey(d => d.UserGroupId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Credential_UserGroup");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Tinnhanh>(entity =>
            {
                entity.HasKey(e => e.Matinnhanh)
                    .HasName("PK_tinnhanh");

                entity.ToTable("tinnhanh");

                entity.HasIndex(e => e.Machuyenmuc)
                    .HasName("IX_tinnhanh_machuyenmuc");

                entity.Property(e => e.Matinnhanh).HasColumnName("matinnhanh");

                entity.Property(e => e.Machuyenmuc).HasColumnName("machuyenmuc");

                entity.Property(e => e.Noidung)
                    .IsRequired()
                    .HasColumnName("noidung");

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

                entity.HasIndex(e => e.Machuyenmuc)
                    .HasName("IX_tintuc_machuyenmuc");

                entity.Property(e => e.Matintuc).HasColumnName("matintuc");

                entity.Property(e => e.Anh)
                    .IsRequired()
                    .HasColumnName("anh")
                    .HasMaxLength(500);

                entity.Property(e => e.Machuyenmuc).HasColumnName("machuyenmuc");

                entity.Property(e => e.Ngaydang)
                    .HasColumnName("ngaydang")
                    .HasColumnType("datetime");

                entity.Property(e => e.Noidung)
                    .IsRequired()
                    .HasColumnName("noidung");

                entity.Property(e => e.Tacgia)
                    .IsRequired()
                    .HasColumnName("tacgia")
                    .HasMaxLength(50);

                entity.Property(e => e.Tieude)
                    .IsRequired()
                    .HasColumnName("tieude")
                    .HasMaxLength(500);

                entity.Property(e => e.Tieudecon)
                    .IsRequired()
                    .HasColumnName("tieudecon")
                    .HasMaxLength(500);

                entity.HasOne(d => d.MachuyenmucNavigation)
                    .WithMany(p => p.Tintuc)
                    .HasForeignKey(d => d.Machuyenmuc)
                    .HasConstraintName("FK_tintuc_chuyenmuc");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.Manguoidung, e.Taikhoan })
                    .HasName("PK_User_1");

                entity.Property(e => e.Manguoidung)
                    .HasColumnName("manguoidung")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Taikhoan)
                    .HasColumnName("taikhoan")
                    .HasColumnType("char(30)");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Email).HasColumnType("char(100)");

                entity.Property(e => e.Groupid)
                    .HasColumnName("groupid")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'member'");

                entity.Property(e => e.Hoten)
                    .IsRequired()
                    .HasColumnName("hoten")
                    .HasMaxLength(50);

                entity.Property(e => e.IsSupper).HasColumnName("isSupper");

                entity.Property(e => e.Matkhau)
                    .IsRequired()
                    .HasColumnName("matkhau")
                    .HasColumnType("nchar(10)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Groupid)
                    .HasConstraintName("FK_User_UserGroup");
            });

            modelBuilder.Entity<UserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");
            });

            modelBuilder.Entity<UserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });
        }
    }
}