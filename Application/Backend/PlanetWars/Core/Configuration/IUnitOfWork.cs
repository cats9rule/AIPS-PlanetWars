using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlanetWars.Core.IRepositories;

namespace PlanetWars.Core.Configuration
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPlayerRepository Players { get; }
        IPlayerColorRepository PlayerColors { get; }
        IColorRepository Colors { get; }
        IPlanetRepository Planets { get; }
        IGalaxyRepository Galaxies { get; }
        ISessionRepository Sessions { get; }
        Task CompleteAsync();
    }
}