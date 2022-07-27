using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class GalaxyDto
    {
        public Guid ID { get; set; }

        public List<Guid> Planets { get; set; }

        public int PlanetCount { get; set; }

        public float ResourcePlanetRatio { get; set; }

        public Guid GameMapID { get; set; }
    }
}