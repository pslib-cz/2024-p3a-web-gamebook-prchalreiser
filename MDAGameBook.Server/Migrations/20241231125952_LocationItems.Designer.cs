﻿// <auto-generated />
using System;
using GameBookASP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MDAGameBook.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241231125952_LocationItems")]
    partial class LocationItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("GameBookASP.GameModels.FightMinigame", b =>
                {
                    b.Property<Guid>("MinigameID")
                        .HasColumnType("TEXT");

                    b.Property<int>("EnemyHealth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EnemyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EnemyStrength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StatPenalty")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MinigameID");

                    b.ToTable("FightMinigames");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Effect")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FightMinigameMinigameID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDrinkable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PlayerID")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemID");

                    b.HasIndex("FightMinigameMinigameID");

                    b.HasIndex("PlayerID");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            ItemID = 1,
                            Description = "A mysterious key that unlocks the path forward.",
                            Effect = "{}",
                            IsDrinkable = false,
                            Name = "Magic Key",
                            Price = 0
                        });
                });

            modelBuilder.Entity("GameBookASP.GameModels.Link", b =>
                {
                    b.Property<int>("LinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Condition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FromLocationID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RequiredItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToLocationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LinkID");

                    b.HasIndex("FromLocationID");

                    b.HasIndex("ToLocationID");

                    b.ToTable("Links");

                    b.HasData(
                        new
                        {
                            LinkID = 1,
                            FromLocationID = 420,
                            RequiredItemId = 1,
                            ToLocationID = 421
                        },
                        new
                        {
                            LinkID = 2,
                            FromLocationID = 421,
                            ToLocationID = 422
                        },
                        new
                        {
                            LinkID = 3,
                            FromLocationID = 421,
                            ToLocationID = 423
                        },
                        new
                        {
                            LinkID = 4,
                            FromLocationID = 421,
                            ToLocationID = 424
                        },
                        new
                        {
                            LinkID = 5,
                            FromLocationID = 422,
                            ToLocationID = 421
                        },
                        new
                        {
                            LinkID = 6,
                            FromLocationID = 423,
                            ToLocationID = 421
                        },
                        new
                        {
                            LinkID = 7,
                            FromLocationID = 424,
                            ToLocationID = 421
                        });
                });

            modelBuilder.Entity("GameBookASP.GameModels.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BackgroundImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasRequiredItem")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LocationID");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationID = -1,
                            BackgroundImageUrl = "https://localhost:7260/Uploads/f5f2add3-d635-4319-8e27-d9494c03b14e.png",
                            Description = "Nacházíš se někde, kam ses neměl dostat :O",
                            HasRequiredItem = false,
                            Items = "[]",
                            Name = "Unlinked Location"
                        },
                        new
                        {
                            LocationID = 420,
                            BackgroundImageUrl = "https://localhost:7260/Uploads/06dfd75a-1c7b-42a2-942d-ee3d48a26a0f.png",
                            Description = "Jsi v interiéru auta a dáváš hotbox.",
                            HasRequiredItem = true,
                            Items = "[1]",
                            Name = "Hotbox"
                        },
                        new
                        {
                            LocationID = 421,
                            BackgroundImageUrl = "https://localhost:7260/Uploads/dark-forest.png",
                            Description = "Vylezl jsi z auta a stojíš v temném lese. Cesta se rozděluje na několik směrů.",
                            HasRequiredItem = false,
                            Items = "[]",
                            Name = "Dark Forest"
                        },
                        new
                        {
                            LocationID = 422,
                            BackgroundImageUrl = "https://localhost:7260/Uploads/cabin.png",
                            Description = "Narazil jsi na starou dřevěnou chatu. Vypadá opuštěně, ale světlo uvnitř stále svítí.",
                            HasRequiredItem = false,
                            Items = "[]",
                            Name = "Abandoned Cabin"
                        },
                        new
                        {
                            LocationID = 423,
                            BackgroundImageUrl = "https://localhost:7260/Uploads/lake.png",
                            Description = "Přišel jsi k temnému jezeru. Měsíční světlo se odráží na jeho hladině.",
                            HasRequiredItem = false,
                            Items = "[]",
                            Name = "Mysterious Lake"
                        },
                        new
                        {
                            LocationID = 424,
                            BackgroundImageUrl = "https://localhost:7260/Uploads/stone-circle.png",
                            Description = "Objevil jsi kruh prastarých kamenů. Ve vzduchu je cítit magická energie.",
                            HasRequiredItem = false,
                            Items = "[]",
                            Name = "Ancient Stone Circle"
                        });
                });

            modelBuilder.Entity("GameBookASP.GameModels.Minigame", b =>
                {
                    b.Property<Guid>("MinigameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LocationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MinigameID");

                    b.HasIndex("LocationID");

                    b.ToTable("Minigames");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Player", b =>
                {
                    b.Property<Guid>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Coins")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Stamina")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Withdrawal")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerID");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerID = new Guid("11111111-1111-1111-1111-111111111111"),
                            Coins = 0,
                            Health = 100,
                            Name = "",
                            Stamina = 100,
                            Withdrawal = 0
                        });
                });

            modelBuilder.Entity("GameBookASP.GameModels.Shop", b =>
                {
                    b.Property<Guid>("ShopID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemsForSale")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LocationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ShopID");

                    b.HasIndex("LocationID");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("GameBookASP.GameModels.UserPlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPlayers");
                });

            modelBuilder.Entity("GameBookASP.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileType")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("GameBookASP.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("GameBookASP.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "da71bb61-65fd-4758-ab0b-d85314217dbf",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "833d85c1-9359-4fb3-8fdc-7611ab20303a",
                            Email = "admin@minjiya.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MINJIYA.COM",
                            NormalizedUserName = "ADMIN@MINJIYA.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGZywXvlM2jRgAbvsCNaEm0D3sX9K8hNRoaEzB9A4clSAqFV19hnMNyNhtNn0e8htw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin@minjiya.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = "50ccf4ea-1c40-456b-81a1-e958d0e7dfc2",
                            UserId = "da71bb61-65fd-4758-ab0b-d85314217dbf"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<string>("RolesId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UsersId")
                        .HasColumnType("TEXT");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("GameBookASP.GameModels.FightMinigame", b =>
                {
                    b.HasOne("GameBookASP.GameModels.Minigame", "Minigame")
                        .WithMany()
                        .HasForeignKey("MinigameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Minigame");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Item", b =>
                {
                    b.HasOne("GameBookASP.GameModels.FightMinigame", null)
                        .WithMany("VictoryReward")
                        .HasForeignKey("FightMinigameMinigameID");

                    b.HasOne("GameBookASP.GameModels.Player", null)
                        .WithMany("Inventory")
                        .HasForeignKey("PlayerID");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Link", b =>
                {
                    b.HasOne("GameBookASP.GameModels.Location", "FromLocation")
                        .WithMany()
                        .HasForeignKey("FromLocationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GameBookASP.GameModels.Location", "ToLocation")
                        .WithMany()
                        .HasForeignKey("ToLocationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FromLocation");

                    b.Navigation("ToLocation");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Minigame", b =>
                {
                    b.HasOne("GameBookASP.GameModels.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Shop", b =>
                {
                    b.HasOne("GameBookASP.GameModels.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("GameBookASP.GameModels.UserPlayer", b =>
                {
                    b.HasOne("GameBookASP.GameModels.Player", "Player")
                        .WithMany("UserPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBookASP.Models.User", "User")
                        .WithMany("UserPlayers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("GameBookASP.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GameBookASP.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GameBookASP.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("GameBookASP.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBookASP.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GameBookASP.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("GameBookASP.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBookASP.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameBookASP.GameModels.FightMinigame", b =>
                {
                    b.Navigation("VictoryReward");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Player", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("UserPlayers");
                });

            modelBuilder.Entity("GameBookASP.Models.User", b =>
                {
                    b.Navigation("UserPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
