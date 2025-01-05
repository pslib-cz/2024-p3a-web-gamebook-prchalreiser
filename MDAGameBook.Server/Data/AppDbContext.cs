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
                options.HasData
                (
                    new Location
                    {
                        LocationID = -1,
                        Name = "Unlinked Location",
                        Description = "Nacházíš se někde, kam ses neměl dostat :O",
                        BackgroundImageUrl = "https://localhost:7260/Uploads/f5f2add3-d635-4319-8e27-d9494c03b14e.png",
                        HasRequiredItem = false
                    },
                    new Location
                    {
                        LocationID = 420,
                        Name = "Hotbox",
                        Description = "Jsi v interiéru auta a dáváš hotbox.",
                        BackgroundImageUrl = "https://localhost:7260/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png",
                        HasRequiredItem = true,
                        Items = "[1]"
                    },
                    new Location
                    {
                        LocationID = 421,
                        Name = "Dark Forest",
                        Description = "Vylezl jsi z auta a stojíš v temném lese. Cesta se rozděluje na několik směrů.",
                        BackgroundImageUrl = "https://localhost:7260/Uploads/dark-forest.png",
                        HasRequiredItem = false,
                    },
                    new Location
                    {
                        LocationID = 422,
                        Name = "Abandoned Cabin",
                        Description = "Narazil jsi na starou dřevěnou chatu. Vypadá opuštěně, ale světlo uvnitř stále svítí.",
                        BackgroundImageUrl = "https://localhost:7260/Uploads/cabin.png",
                        HasRequiredItem = false
                    },
                    new Location
                    {
                        LocationID = 423,
                        Name = "Mysterious Lake",
                        Description = "Přišel jsi k temnému jezeru. Měsíční světlo se odráží na jeho hladině.",
                        BackgroundImageUrl = "https://localhost:7260/Uploads/lake.png",
                        HasRequiredItem = false
                    },
                    new Location
                    {
                        LocationID = 424,
                        Name = "Ancient Stone Circle",
                        Description = "Objevil jsi kruh prastarých kamenů. Ve vzduchu je cítit magická energie.",
                        BackgroundImageUrl = "https://localhost:7260/Uploads/stone-circle.png",
                        HasRequiredItem = false
                    }
                );
            });
            builder.Entity<Item>().HasData(
                new Item
                {
                    ItemID = 1,
                    Name = "Magic Key",
                    Description = "A mysterious key that unlocks the path forward.",
                    Price = 0,
                    IsDrinkable = false,
                    Effect = "{}" // No effects for now
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
                    .HasForeignKey(e => e.FromLocationID)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasOne(e => e.ToLocation)
                    .WithMany()
                    .HasForeignKey(e => e.ToLocationID)
                    .OnDelete(DeleteBehavior.Restrict);

                options.HasData(
                    new Link
                    {
                        LinkID = 1,
                        FromLocationID = 420,
                        ToLocationID = 421,
                        RequiredItemId = 1
                    },
                    new Link
                    {
                        LinkID = 2,
                        FromLocationID = 421,
                        ToLocationID = 422,
                        RequiredItemId = null
                    },
                    new Link
                    {
                        LinkID = 3,
                        FromLocationID = 421,
                        ToLocationID = 423,
                        RequiredItemId = null
                    },
                    new Link
                    {
                        LinkID = 4,
                        FromLocationID = 421,
                        ToLocationID = 424,
                        RequiredItemId = null
                    },
                    new Link
                    {
                        LinkID = 5,
                        FromLocationID = 422,
                        ToLocationID = 421,
                        RequiredItemId = null
                    },
                    new Link
                    {
                        LinkID = 6,
                        FromLocationID = 423,
                        ToLocationID = 421,
                        RequiredItemId = null
                    },
                    new Link
                    {
                        LinkID = 7,
                        FromLocationID = 424,
                        ToLocationID = 421,
                        RequiredItemId = null
                    }
                );
            });
        }
    }
}
