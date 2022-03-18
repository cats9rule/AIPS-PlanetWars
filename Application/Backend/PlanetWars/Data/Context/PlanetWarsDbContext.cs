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
        public DbSet<GameMap> GameMaps { get; set; }

        public DbSet<PlanetPlanet> PlanetPlanet { get; set; }

        public PlanetWarsDbContext(DbContextOptions<PlanetWarsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanetPlanet>()
                .HasKey(pp => pp.ID);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithMany(u => u.Players)
                .HasForeignKey(p => p.UserID).IsRequired(true)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Session)
                .WithMany(s => s.Players)
                .HasForeignKey(p => p.SessionID).IsRequired(true)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            modelBuilder.Entity<Galaxy>()
                .HasOne(g => g.Session)
                .WithOne(s => s.Galaxy)
                .HasForeignKey<Galaxy>(g => g.SessionID).IsRequired(true)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Planet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Planets)
                .HasForeignKey(p => p.OwnerID).IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Planet>()
                .HasOne(p => p.Galaxy)
                .WithMany(g => g.Planets)
                .HasForeignKey(p => p.GalaxyID).IsRequired(true)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}