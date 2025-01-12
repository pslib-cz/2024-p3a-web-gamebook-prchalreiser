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
    [Migration("20250112123600_smrdiprdi")]
    partial class smrdiprdi
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
                        },
                        new
                        {
                            ItemID = 4,
                            Description = "A mystical crystal that instantly restores 50 health points.",
                            Effect = "{\"health\": 50}",
                            IsDrinkable = true,
                            Name = "Healing Crystal",
                            Price = 100
                        },
                        new
                        {
                            ItemID = 5,
                            Description = "A fizzy drink that restores 40 stamina points.",
                            Effect = "{\"stamina\": 40}",
                            IsDrinkable = true,
                            Name = "Energy Drink",
                            Price = 75
                        },
                        new
                        {
                            ItemID = 6,
                            Description = "A bitter potion that reduces withdrawal effects by 30 points.",
                            Effect = "{\"withdrawal\": -30}",
                            IsDrinkable = true,
                            Name = "Anti-Withdrawal Potion",
                            Price = 150
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

                    b.Property<bool>("HasShop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LocationID");

                    b.ToTable("Locations");
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

                    b.Property<int>("LastLocationID")
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
                            LastLocationID = -1,
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

                    b.HasIndex("LocationID")
                        .IsUnique();

                    b.ToTable("Shops");

                    b.HasData(
                        new
                        {
                            ShopID = new Guid("22222222-2222-2222-2222-222222222222"),
                            ItemsForSale = "[]",
                            LocationID = 69
                        });
                });

            modelBuilder.Entity("GameBookASP.GameModels.ShopItem", b =>
                {
                    b.Property<int>("ShopItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ShopID")
                        .HasColumnType("TEXT");

                    b.HasKey("ShopItemID");

                    b.HasIndex("ItemID");

                    b.HasIndex("ShopID");

                    b.ToTable("ShopItem");

                    b.HasData(
                        new
                        {
                            ShopItemID = 4,
                            ItemID = 4,
                            Price = 100,
                            Quantity = 3,
                            ShopID = new Guid("22222222-2222-2222-2222-222222222222")
                        },
                        new
                        {
                            ShopItemID = 5,
                            ItemID = 5,
                            Price = 75,
                            Quantity = 5,
                            ShopID = new Guid("22222222-2222-2222-2222-222222222222")
                        },
                        new
                        {
                            ShopItemID = 6,
                            ItemID = 6,
                            Price = 150,
                            Quantity = 2,
                            ShopID = new Guid("22222222-2222-2222-2222-222222222222")
                        });
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
                            Id = "0718c044-bd98-4abd-ae94-12ac62941479",
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
                            Id = "31ba445d-daaf-4fff-bd7e-32aba16ee02b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fb5a51bb-25c6-45b7-88c4-228fc2f2975f",
                            Email = "admin@minjiya.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@MINJIYA.COM",
                            NormalizedUserName = "ADMIN@MINJIYA.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEJX4rVHZbDpNtBJtSugxOdJVKMu+TdwtI3U/c/wwFI8LdpG7ov/5v7gMZa3H5wsC6A==",
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
                            RoleId = "0718c044-bd98-4abd-ae94-12ac62941479",
                            UserId = "31ba445d-daaf-4fff-bd7e-32aba16ee02b"
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBookASP.GameModels.Location", "ToLocation")
                        .WithMany()
                        .HasForeignKey("ToLocationID")
                        .OnDelete(DeleteBehavior.Cascade)
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
                        .WithOne("Shop")
                        .HasForeignKey("GameBookASP.GameModels.Shop", "LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("GameBookASP.GameModels.ShopItem", b =>
                {
                    b.HasOne("GameBookASP.GameModels.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBookASP.GameModels.Shop", "Shop")
                        .WithMany("ShopItems")
                        .HasForeignKey("ShopID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Shop");
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

            modelBuilder.Entity("GameBookASP.GameModels.Location", b =>
                {
                    b.Navigation("Shop");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Player", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("UserPlayers");
                });

            modelBuilder.Entity("GameBookASP.GameModels.Shop", b =>
                {
                    b.Navigation("ShopItems");
                });

            modelBuilder.Entity("GameBookASP.Models.User", b =>
                {
                    b.Navigation("UserPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
