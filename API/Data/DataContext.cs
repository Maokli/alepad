using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
      IdentityRoleClaim<int>, IdentityUserToken<int>>
  {
    public DataContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<AppUser>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();

      builder.Entity<AppUser>()
        .Property(u => u.EmailConfirmed)
        .HasConversion(
          v => v ? 1 : 0,
          v => (v == 1)
        );

      builder.Entity<AppUser>()
        .Property(u => u.LockoutEnabled)
        .HasConversion(
          v => v ? 1 : 0,
          v => (v == 1)
        );

      builder.Entity<AppUser>()
        .Property(u => u.PhoneNumberConfirmed)
        .HasConversion(
          v => v ? 1 : 0,
          v => (v == 1)
        );

      builder.Entity<AppUser>()
        .Property(u => u.TwoFactorEnabled)
        .HasConversion(
          v => v ? 1 : 0,
          v => (v == 1)
        );

      builder.Entity<AppRole>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.Role)
        .HasForeignKey(ur => ur.RoleId)
        .IsRequired();

      
    }
  }
}