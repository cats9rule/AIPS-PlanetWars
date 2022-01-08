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

            modelBuilder.Entity("PlanetWars.Data.Models.Color", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HexValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Galaxy", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<Guid?>("PlanetID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("GalaxyID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("PlanetID");

                    b.ToTable("Planet");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("SessionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ColorID");

                    b.HasIndex("SessionID");

                    b.HasIndex("UserID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.PlayerColor", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TurnIndex")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ColorID");

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

                    b.HasOne("PlanetWars.Data.Models.Planet", null)
                        .WithMany("NeighbourPlanets")
                        .HasForeignKey("PlanetID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.Player", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.PlayerColor", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("PlanetWars.Data.Models.Session", null)
                        .WithMany("Players")
                        .HasForeignKey("SessionID");

                    b.HasOne("PlanetWars.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Color");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanetWars.Data.Models.PlayerColor", b =>
                {
                    b.HasOne("PlanetWars.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.Navigation("Color");
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
