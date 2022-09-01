using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PlanetWars.Core.Configuration;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.ConcreteServices
{
    public class PlanetService : IPlanetService
    {
        private int atkBoost = 2;
        private int defBoost = 2;
        private int movBoost = 2;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlanetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Add(PlanetDto planetDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Add(_mapper.Map<PlanetDto, Planet>(planetDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlanetDto>> CreatePlanets(CreateGameDto createGameDto, Guid GalaxyID, Guid SessionID)
        {
            using (_unitOfWork)
            {
                GameMap gameMap = await _unitOfWork.GameMaps.GetById(createGameDto.GameMapID);
                List<Planet> planetList = new List<Planet>();

                int planetsWithResource = (int)(gameMap.PlanetCount * gameMap.ResourcePlanetRatio);

                for (int i = 0; i < gameMap.PlanetCount; i++)
                {
                    Planet planet = i < planetsWithResource ? await CreatePlanet(true, -1, GalaxyID) : await CreatePlanet(false, -1, GalaxyID);
                    planetList.Add(planet);


                }
                ShufflePlanets(planetList);
                var retval = await _unitOfWork.Planets.AddMany(planetList);

                //await _unitOfWork.CompleteAsync();

                GameMapDto gameMapDto = _mapper.Map<GameMapDto>(gameMap);
                foreach (var entry in gameMapDto.PlanetGraph)
                {
                    foreach (int index in entry.Value)
                    {
                        PlanetPlanet pp = new PlanetPlanet()
                        {
                            PlanetFromID = planetList[Int32.Parse(entry.Key)].ID,
                            PlanetToID = planetList[index].ID,
                            SessionID = SessionID
                        };
                        await _unitOfWork.PlanetPlanets.Add(pp);
                    }
                }
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<List<PlanetDto>>(planetList);
            }
        }



        public async Task<bool> Delete(Guid id)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Delete(id);
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlanetDto>> GetAll()
        {
            using (_unitOfWork)
            {
                List<Planet> planets = new List<Planet>(await _unitOfWork.Planets.GetAll());
                return _mapper.Map<List<Planet>, List<PlanetDto>>(planets);
            }
        }

        public async Task<PlanetDto> GetById(Guid id)
        {
            using (_unitOfWork)
            {
                var planet = await _unitOfWork.Planets.GetById(id);
                if (planet != null)
                {
                    return _mapper.Map<Planet, PlanetDto>(planet);
                }
                return null;
            }
        }

        public async Task<IEnumerable<PlanetDto>> GetForPlayer(Guid playerId)
        {
            using (_unitOfWork)
            {
                var player = await _unitOfWork.Players.GetById(playerId);
                if (player != null)
                {
                    return _mapper.Map<ICollection<Planet>, List<PlanetDto>>(player.Planets);
                }
                return new List<PlanetDto>();
            }
        }

        public async Task<bool> Update(PlanetDto planetDto)
        {
            using (_unitOfWork)
            {
                var retval = await _unitOfWork.Planets.Update(_mapper.Map<PlanetDto, Planet>(planetDto));
                await _unitOfWork.CompleteAsync();
                return retval;
            }
        }

        public async Task<IEnumerable<PlanetDto>> GetRelatedPlanets(Guid planetID)
        {
            using (_unitOfWork)
            {
                var relations = await _unitOfWork.PlanetPlanets.GetAllRelationsForPlanet(planetID);
                List<PlanetDto> relatedPlanets = new List<PlanetDto>();
                foreach (PlanetPlanet relation in relations)
                {
                    if (relation.PlanetFromID == null) return null;
                    var planet = await _unitOfWork.Planets.GetById(relation.PlanetFromID);
                    if (planet != null)
                    {
                        relatedPlanets.Add(_mapper.Map<Planet, PlanetDto>(planet));
                    }
                }
                return relatedPlanets;
            }
        }

        public async Task<bool> DeleteAll()
        {
            using (_unitOfWork)
            {
                await _unitOfWork.PlanetPlanets.DeleteAll();
                await _unitOfWork.Planets.DeleteAll();
                await _unitOfWork.CompleteAsync();
                return true;
            }
        }
        private async Task<Planet> CreatePlanet(bool hasResource, int planetIndex, Guid GalaxyID)
        {
            Planet planet = new Planet()
            {
                ArmyCount = 0,
                Owner = null,
                OwnerID = null,
                IndexInGalaxy = planetIndex,
                GalaxyID = GalaxyID,
                Galaxy = await _unitOfWork.Galaxies.GetById(GalaxyID),
                Extras = ""
            };
            bool madeResource = !hasResource;
            while (!madeResource)
            {
                Random rnd = new Random();
                int num = rnd.Next();
                if (num % 3 == 0)
                {
                    planet.Extras += "def,";
                    madeResource = true;
                }
                if (num % 4 == 0)
                {
                    planet.Extras += "atk,";
                    madeResource = true;
                }
                if (num % 5 == 0)
                {
                    planet.Extras = "mov,";
                    madeResource = true;
                }
            }
            return planet;
        }

        #region Helpers 
        private List<Planet> ShufflePlanets(List<Planet> planets)
        {
            int count = planets.Count;
            Random rnd = new Random(count);
            for (int i = 0; i < count; i++)
            {
                int next = rnd.Next() % count;
                Planet p = planets[i];
                planets[i] = planets[next];
                planets[next] = p;
            }
            for (int i = 0; i < count; i++)
            {
                planets[i].IndexInGalaxy = i;
            }
            return planets;
        }
        #endregion
    }
}