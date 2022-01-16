using Microsoft.EntityFrameworkCore;
using PlanetWars.Data.Models;

namespace PlanetWars.Data.Context
{
    public class PlanetWarsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerColor> PlayerColors { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Galaxy> Galaxies { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<PlanetPlanet> PlanetPlanet { get; set; }

        public PlanetWarsDbContext(DbContextOptions<PlanetWarsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanetPlanet>()
                .HasKey(pp => new { pp.PlanetFromID, pp.PlanetToID });

            modelBuilder.Entity<PlanetPlanet>()
                .HasOne(pp => pp.PlanetFrom)
                .WithMany() 
                .HasForeignKey(pp => pp.PlanetFromID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PlanetPlanet>()
                .HasOne(pp => pp.PlanetTo)
                .WithMany(p => p.NeighbourPlanets)
                .HasForeignKey(pp => pp.PlanetToID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithMany(u => u.Players)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Session)
                .WithMany(s => s.Players)
                .HasForeignKey(p => p.SessionID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            modelBuilder.Entity<Galaxy>()
                .HasOne(g => g.Session)
                .WithOne(s => s.Galaxy)
                .HasForeignKey<Galaxy>(g => g.SessionID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Planet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Planets)
                .HasForeignKey(p => p.OwnerID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Planet>()
                .HasOne(p => p.Galaxy)
                .WithMany(g => g.Planets)
                .HasForeignKey(p => p.GalaxyID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}