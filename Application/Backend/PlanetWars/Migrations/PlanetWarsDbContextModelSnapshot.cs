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

                    b.Property<int>("PlanetCount")
                        .HasColumnType("int");

                    b.Property<float>("ResourcePlanetRatio")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Galaxy");
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

                    b.Property<Guid?>("GalaxyID")
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("PlanetFromID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanetToID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlanetFromID", "PlanetToID");

                    b.HasIndex("PlanetToID");

                    b.ToTable("PlanetPlanet");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PlayerColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SessionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserID")
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

                    b.Property<Guid?>("CreatorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GalaxyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PlayerOnTurnID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("CreatorID");

                    b.HasIndex("GalaxyID");

                    b.HasIndex("PlayerOnTurnID");

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

            modelBuilder.Entity("PlanetWars.Data.Models.Planet", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.Galaxy", null)
                        .WithMany("Planets")
                        .HasForeignKey("GalaxyID");

                    b.HasOne("PlanetWars.Data.Models.Player", "Owner")
                        .WithMany("Planets")
                        .HasForeignKey("OwnerID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.PlanetPlanet", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.Planet", "PlanetFrom")
                        .WithMany()
                        .HasForeignKey("PlanetFromID")
                        .IsRequired();

                    b.HasOne("PlanetWars.Data.Models.Planet", "PlanetTo")
                        .WithMany("NeighbourPlanets")
                        .HasForeignKey("PlanetToID")
                        .IsRequired();

                    b.Navigation("PlanetFrom");

                    b.Navigation("PlanetTo");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.PlayerColor", "PlayerColor")
                        .WithMany()
                        .HasForeignKey("PlayerColorID");

                    b.HasOne("PlanetWars.Data.Models.Session", null)
                        .WithMany("Players")
                        .HasForeignKey("SessionID");

                    b.HasOne("PlanetWars.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("PlayerColor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Session", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.Player", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorID");

                    b.HasOne("PlanetWars.Data.Models.Galaxy", "Galaxy")
                        .WithMany()
                        .HasForeignKey("GalaxyID");

                    b.HasOne("PlanetWars.Data.Models.Player", "PlayerOnTurn")
                        .WithMany()
                        .HasForeignKey("PlayerOnTurnID");

                    b.Navigation("Creator");

                    b.Navigation("Galaxy");

                    b.Navigation("PlayerOnTurn");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Galaxy", b =>
                {
                    b.Navigation("Planets");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Planet", b =>
                {
                    b.Navigation("NeighbourPlanets");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.Navigation("Planets");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Session", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
