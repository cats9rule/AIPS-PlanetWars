using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class GalaxyDto
    {
        public Guid ID { get; set; }

        public List<PlanetDto> Planets { get; set; }

        public GameMapDto GameMap { get; set; }
    }
}