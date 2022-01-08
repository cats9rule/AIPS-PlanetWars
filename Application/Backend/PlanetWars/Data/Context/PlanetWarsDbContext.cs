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

        public PlanetWarsDbContext(DbContextOptions<PlanetWarsDbContext> options) : base(options)
        {
            
        }
    }
}