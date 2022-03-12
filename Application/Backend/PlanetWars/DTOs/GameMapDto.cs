using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class GameMapDto
    {
        public Guid ID { get; set; }
        public Dictionary<int, List<int>> PlanetGraph { get; set; }
        public List<bool> PlanetMatrix { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int PlanetCount { get; set; }
    }
}