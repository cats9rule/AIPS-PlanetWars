using System;
using System.Collections.Generic;

namespace PlanetWars.Data.Models
{
    public class GameMap
    {
        public Guid ID { get; set; }
        public int PlanetCount { get; set; }
        public float ResourcePlanetRatio { get; set; }
        public int MaxPlayers { get; set; }
        public string PlanetGraph { get; set; }
        public string Description { get; set; }
    }
}