using Microsoft.EntityFrameworkCore;
using PlanetWars.Data.Models;

namespace PlanetWars.Data.Context
{
    public class PlanetWarsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerColor> PlayerColors { get; set; }
        public DbSet<Color> Colors { get; set; }
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
        }
    }
}