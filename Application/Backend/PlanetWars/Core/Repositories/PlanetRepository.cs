using PlanetWars.Core.IRepositories;
using PlanetWars.Data.Models;
using PlanetWars.Data.Context;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PlanetWars.Core.Repositories
{
    public class PlanetRepository : GenericRepository<Planet>, IPlanetRepository
    {
        public PlanetRepository(PlanetWarsDbContext context) : base(context)
        {

        }

        public override Task<bool> Add(Planet entity)
        {
            return base.Add(entity);
        }

        public override Task<IEnumerable<Planet>> GetAll()
        {
            return base.GetAll();
        }

        public override Task<Planet> GetById(Guid id)
        {
            return base.GetById(id);
        }

        public override async Task<bool> Delete(Guid id)
        {
            var exist = await dbSet.Where(x => x.ID == id).FirstOrDefaultAsync();
            if(exist != null)
            {
                dbSet.Remove(exist);
                return true;
            }
            return false;
        }

        public override async Task<bool> Update(Planet entity)
        {
            var exist = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();
            if(exist == null)
                return await Add(entity);
            dbSet.Update(entity);
            return true;
        }

        public async Task<IEnumerable<Planet>> GetForPlayer(Guid playerID)
        {
            return await dbSet.Where(p => p.Owner.ID == playerID).ToListAsync();
        }

        public async Task<bool> UpdateArmies(Guid id, int armyDifference)
        {
            var planet = await dbSet.FindAsync(id);
            if (planet != null)
            {
                planet.ArmyCount += armyDifference;
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOwnership(Guid id, Guid ownerID)
        {
            var planet = await dbSet.FindAsync(id);
            if (planet != null) {
                planet.OwnerID = ownerID;
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOwnership(int planetIndex, Guid galaxyID, Guid ownerID) {
            Console.WriteLine("\n\n" + galaxyID);
            Console.WriteLine(ownerID + "\n\n");
            var planet = await dbSet.Where(p => p.GalaxyID == galaxyID && p.IndexInGalaxy == planetIndex).FirstOrDefaultAsync();

            if (planet != null) {
                Console.WriteLine("\n\n Planet is not null. " + planet.ID + "\n\n");
                planet.OwnerID = ownerID;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Planet>> GetForSession(Guid sessionID)
        {
            return await dbSet.Where(p => p.Galaxy.SessionID == sessionID).ToListAsync();
        }
    }
}