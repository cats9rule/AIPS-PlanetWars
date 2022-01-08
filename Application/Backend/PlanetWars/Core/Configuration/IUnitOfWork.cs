using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetWars.Core.Configuration
{
    public interface IUnitOfWork
    {
        //TODO: Add IRepositories
        
        Task CompleteAsync();
    }
}