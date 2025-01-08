using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GameBookASP.GameModels;

namespace GameBookASP.Data
{
    public class AppDbContext : IdentityDbContext<Models.User, Models.Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player>? Players { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Link>? Links { get; set; }
        public DbSet<Shop>? Shops { get; set; }
        public DbSet<Item>? Items { get; set; }
        public DbSet<Minigame>? Minigames { get; set; }
        public DbSet<FightMinigame>? FightMinigames { get; set; }

        public DbSet<Models.File>? Files { get; set; }
        public override DbSet<Models.User> Users { get; set; } = null!;
        public DbSet<UserPlayer>? UserPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminRoleId = Guid.NewGuid().ToString();
            builder.Entity<Models.Role>(options =>
            {
                options.HasData(
                       new Models.Role
                       {
                           Id = adminRoleId,
                           Name = "Admin",
                           NormalizedName = "ADMIN"
                       }
                );
            });

            var adminId = Guid.NewGuid().ToString();
            builder.Entity<Models.User>(options =>
            {
                options.HasData(
                    new Models.User
                    {
                        Id = adminId,
                        UserName = "admin@minjiya.com",
                        NormalizedUserName = "ADMIN@MINJIYA.COM",
                        Email = "admin@minjiya.com",
                        NormalizedEmail = "ADMIN@MINJIYA.COM",
                        EmailConfirmed = true,
                        PasswordHash = new PasswordHasher<Models.User>().HashPassword(null!, "Ps!l0cyb!n"),
                        SecurityStamp = string.Empty
                    }
                );
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(x => new { x.RoleId, x.UserId });
                entity.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = adminRoleId,
                        UserId = adminId
                    }
                );
            });

            builder.Entity<Location>(options =>
            {
                options.HasMany<Link>()
                       .WithOne(l => l.FromLocation)
                       .HasForeignKey(l => l.FromLocationID)
                       .OnDelete(DeleteBehavior.Cascade);

                options.HasMany<Link>()
                       .WithOne(l => l.ToLocation)
                       .HasForeignKey(l => l.ToLocationID)
                       .OnDelete(DeleteBehavior.Cascade);

            });
            builder.Entity<Item>().HasData(
                new Item
                {
                    ItemID = 1,
                    Name = "Magic Key",
                    Description = "A mysterious key that unlocks the path forward.",
                    Price = 0,
                    IsDrinkable = false,
                    Effect = "{}"
                },
                new Item
                {
                    ItemID = 4,
                    Name = "Healing Crystal",
                    Description = "A mystical crystal that instantly restores 50 health points.",
                    Price = 100,
                    IsDrinkable = true,
                    Effect = "{\"health\": 50}"
                },
                new Item
                {
                    ItemID = 5,
                    Name = "Energy Drink",
                    Description = "A fizzy drink that restores 40 stamina points.",
                    Price = 75,
                    IsDrinkable = true,
                    Effect = "{\"stamina\": 40}"
                },
                new Item
                {
                    ItemID = 6,
                    Name = "Anti-Withdrawal Potion",
                    Description = "A bitter potion that reduces withdrawal effects by 30 points.",
                    Price = 150,
                    IsDrinkable = true,
                    Effect = "{\"withdrawal\": -30}"
                }
            );

            builder.Entity<Player>().HasData(
                new Player
                {
                    PlayerID = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Example GUID
                    Health = 100,
                    Withdrawal = 0,
                    Stamina = 100,
                    Coins = 0,
                    Inventory = new List<Item>()
                }
            );

            builder.Entity<UserPlayer>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPlayers)
                .HasForeignKey(up => up.UserId);

            builder.Entity<UserPlayer>()
                .HasOne(up => up.Player)
                .WithMany(p => p.UserPlayers)
                .HasForeignKey(up => up.PlayerId);

            builder.Entity<Link>(options =>
            {
                options.HasKey(e => e.LinkID);
                options.HasOne(e => e.FromLocation)
                       .WithMany()
                       .HasForeignKey(e => e.FromLocationID);

                options.HasOne(e => e.ToLocation)
                       .WithMany()
                       .HasForeignKey(e => e.ToLocationID);

            });

            // Seed shop for scene 69
            builder.Entity<Shop>().HasData(
                new Shop
                {
                    ShopID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    LocationID = 69,
                    ItemsForSale = "[]" // Required due to backward compatibility
                }
            );

            // Seed shop items
            builder.Entity<ShopItem>().HasData(
                new ShopItem
                {
                    ShopItemID = 4,
                    ItemID = 4, // Healing Crystal
                    Price = 100,
                    Quantity = 3,
                    ShopID = Guid.Parse("22222222-2222-2222-2222-222222222222") // Link to shop
                },
                new ShopItem
                {
                    ShopItemID = 5,
                    ItemID = 5, // Energy Drink
                    Price = 75,
                    Quantity = 5,
                    ShopID = Guid.Parse("22222222-2222-2222-2222-222222222222") // Link to shop
                },
                new ShopItem
                {
                    ShopItemID = 6,
                    ItemID = 6, // Anti-Withdrawal Potion
                    Price = 150,
                    Quantity = 2,
                    ShopID = Guid.Parse("22222222-2222-2222-2222-222222222222") // Link to shop
                }
            );

            // Seed the location
            builder.Entity<Location>().HasData(
                new
                {
                    LocationID = 69,
                    Name = "Merchant's Corner",
                    Description = "A dimly lit shop filled with mysterious potions and magical items. The merchant watches you with keen interest.",
                    Items = "[]",
                    HasShop = true,
                    HasRequiredItem = false,
                    BackgroundImageUrl = "/images/shop.jpg"
                }
            );
        }
    }
}
