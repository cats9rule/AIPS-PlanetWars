using System;
using System.Threading.Tasks;
using PlanetWars.Core.IRepositories;
using PlanetWars.Core.Repositories;
using PlanetWars.Data.Context;

namespace PlanetWars.Core.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlanetWarsDbContext _context;
        public IUserRepository Users { get; private set; }

        public IPlayerRepository Players { get; private set; }

        public IPlayerColorRepository PlayerColors { get; private set; }

        public IColorRepository Colors { get; private set; }

        public IPlanetRepository Planets { get; private set; }

        public IGalaxyRepository Galaxies { get; private set; }

        public ISessionRepository Sessions { get; private set; }

        public UnitOfWork(PlanetWarsDbContext context)
        {
            _context = context;

            Users = new UserRepository(context);
            Players = new PlayerRepository(context);
            PlayerColors = new PlayerColorRepository(context);
            Colors = new ColorRepository(context);
            Planets = new PlanetRepository(context);
            Galaxies = new GalaxyRepository(context);
            Sessions = new SessionRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}