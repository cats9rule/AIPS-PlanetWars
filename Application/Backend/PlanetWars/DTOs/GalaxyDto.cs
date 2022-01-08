using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class GalaxyDto
    {
        public int ID { get; set; }

        public List<int> Planets { get; set; }
    }
}