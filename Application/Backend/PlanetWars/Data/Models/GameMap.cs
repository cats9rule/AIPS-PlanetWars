using System;
using System.ComponentModel.DataAnnotations;

namespace PlanetWars.Data.Models
{
    public class GameMap
    {
        [Key]
        public Guid ID { get; set; }
        public int PlanetCount { get; set; }
        public float ResourcePlanetRatio { get; set; }
        public int MaxPlayers { get; set; }
        public string PlanetGraph { get; set; }
        public string PlanetMatrix { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Description { get; set; }

    }
}