using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GameBookASP.GameModels;

namespace GameBookASP.Data
{
    public class AppDbContext : IdentityDbContext<Models.User, Models.Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<GameModels.Player>? Players { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Link>? Links { get; set; }
        public DbSet<Shop>? Shops { get; set; }
        public DbSet<Item>? Items { get; set; }
        public DbSet<Minigame>? Minigames { get; set; }

        public DbSet<Models.File>? Files { get; set; }
        public override DbSet<Models.User> Users { get; set; } = null!;
        public DbSet<UserPlayer>? UserPlayers { get; set; }
        public DbSet<PlayerMinigame>? PlayerMinigames { get; set; }
        public DbSet<PlayerItem>? PlayerItems { get; set; }

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
                },
                new Item
                {
                    ItemID = 7,
                    Name = "Mysterious Crystal",
                    Description = "A strange crystal that pulses with an otherworldly energy.",
                    Price = 0,
                    IsDrinkable = false,
                    Effect = "{}"
                }
            );

            builder.Entity<GameBookASP.GameModels.Player>().HasData(
                new GameBookASP.GameModels.Player
                {
                    PlayerID = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Example GUID
                    Health = 100,
                    Withdrawal = 0,
                    Stamina = 100,
                    Coins = 50,
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

            builder.Entity<Location>().HasData(
                new Location
                {
                    LocationID = 55,
                    Name = "Rock Paper Scissors Challenge",
                    Description = "A mysterious figure challenges you to a game of Rock Paper Scissors.",
                    Items = "[]",
                    BackgroundImageUrl = "/images/rps-background.jpg",
                    HasRequiredItem = false,
                    HasShop = false,
                    HasMinigame = true
                },
                new Location
                {
                    LocationID = 101,
                    Name = "Crystal Chamber",
                    Description = "A dimly lit chamber where a mysterious crystal floats in the air.",
                    Items = "[7]",
                    BackgroundImageUrl = "/images/crystal-chamber.jpg",
                    HasRequiredItem = false,
                    HasShop = false,
                    HasMinigame = false
                }
            );

            builder.Entity<Minigame>()
                .HasData(
                    new Minigame
                    {
                        MinigameID = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                        LocationID = 55,
                        Description = "Challenge the mysterious stranger to a game of Rock Paper Scissors! First to 3 wins.",
                        Type = "RPS",
                        OpponentName = "Mysterious Stranger",
                        WinLocationID = 56,
                        LoseLocationID = 57
                    }
                );

            builder.Entity<PlayerMinigame>()
                .HasOne(pm => pm.Player)
                .WithMany()
                .HasForeignKey(pm => pm.PlayerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PlayerMinigame>()
                .HasOne(pm => pm.Minigame)
                .WithMany(m => m.PlayerMinigames)
                .HasForeignKey(pm => pm.MinigameID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
