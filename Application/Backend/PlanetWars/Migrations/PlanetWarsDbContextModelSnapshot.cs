﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanetWars.Data.Context;

namespace PlanetWars.Migrations
{
    [DbContext(typeof(PlanetWarsDbContext))]
    partial class PlanetWarsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlanetWars.Data.Models.Galaxy", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameMapID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PlanetCount")
                        .HasColumnType("int");

                    b.Property<float>("ResourcePlanetRatio")
                        .HasColumnType("real");

                    b.Property<Guid>("SessionID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("SessionID")
                        .IsUnique();

                    b.ToTable("Galaxy");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.GameMap", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Columns")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("PlanetCount")
                        .HasColumnType("int");

                    b.Property<string>("PlanetGraph")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanetMatrix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ResourcePlanetRatio")
                        .HasColumnType("real");

                    b.Property<int>("Rows")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("GameMaps");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Planet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ArmyCount")
                        .HasColumnType("int");

                    b.Property<int>("AttackBoost")
                        .HasColumnType("int");

                    b.Property<int>("DefenseBoost")
                        .HasColumnType("int");

                    b.Property<Guid>("GalaxyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IndexInGalaxy")
                        .HasColumnType("int");

                    b.Property<int>("MovementBoost")
                        .HasColumnType("int");

                    b.Property<Guid?>("OwnerID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("GalaxyID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Planet");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.PlanetPlanet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanetFromID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanetToID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("PlanetPlanet");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSessionOwner")
                        .HasColumnType("bit");

                    b.Property<Guid>("PlayerColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalArmies")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("PlayerColorID");

                    b.HasIndex("SessionID");

                    b.HasIndex("UserID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.PlayerColor", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ColorHexValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TurnIndex")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("PlayerColor");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Session", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentTurnIndex")
                        .HasColumnType("int");

                    b.Property<string>("GameCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerCount")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Galaxy", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.Session", "Session")
                        .WithOne("Galaxy")
                        .HasForeignKey("PlanetWars.Data.Models.Galaxy", "SessionID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Planet", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.Galaxy", "Galaxy")
                        .WithMany("Planets")
                        .HasForeignKey("GalaxyID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("PlanetWars.Data.Models.Player", "Owner")
                        .WithMany("Planets")
                        .HasForeignKey("OwnerID");

                    b.Navigation("Galaxy");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.PlayerColor", "PlayerColor")
                        .WithMany("PlayersWithColor")
                        .HasForeignKey("PlayerColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanetWars.Data.Models.Session", "Session")
                        .WithMany("Players")
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("PlanetWars.Data.Models.User", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PlayerColor");

                    b.Navigation("Session");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Galaxy", b =>
                {
                    b.Navigation("Planets");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.Navigation("Planets");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.PlayerColor", b =>
                {
                    b.Navigation("PlayersWithColor");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Session", b =>
                {
                    b.Navigation("Galaxy");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.User", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
