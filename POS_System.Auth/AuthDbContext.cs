using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_System.Auth.Models;

namespace POS_System.Auth
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<ApplicationUser, Role, Guid>(options)
    {
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the Role entity
            builder.Entity<Role>(role =>
            {
                role.HasKey(r => r.Id);
                role.Property(r => r.Name).IsRequired().HasMaxLength(256);
                role.HasIndex(r => r.NormalizedName).IsUnique();
                role.ToTable("Roles");

                // Configure the relationship between Role and Permission
                role.HasMany(r => r.Permissions)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("RolePermissions"));
            });

            // Configure the Permission entity
            builder.Entity<Permission>(permission =>
            {
                permission.HasKey(p => p.Id);
                permission.Property(p => p.Name).IsRequired().HasMaxLength(256);
                permission.ToTable("Permissions");
            });
        }
    }
}

