using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class PlanetDto
    {
        public int ID { get; set; }

        public string Owner { get; set; }

        public int ArmyCount { get; set; }

        public List<int> NeighbourPlanets {get; set; }
        
    }
}