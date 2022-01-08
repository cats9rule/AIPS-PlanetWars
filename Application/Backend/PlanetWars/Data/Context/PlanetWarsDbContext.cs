using Microsoft.EntityFrameworkCore;

namespace PlanetWars.Data.Context
{
    public class PlanetWarsDbContext : DbContext
    {
        
        public PlanetWarsDbContext(DbContextOptions<PlanetWarsDbContext> options) : base(options)
        {
            
        }
    }
}