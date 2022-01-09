using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class PlanetDto
    {
        public Guid ID { get; set; }

        public Guid Owner { get; set; }

        public int ArmyCount { get; set; }

        public List<Guid> NeighbourPlanets {get; set; }

        public int MovementBoost { get; set; }
        public int AttackBoost { get; set; }
        public int DefenseBoost { get; set; }
        
    }
}