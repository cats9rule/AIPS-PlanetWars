using System;

namespace PlanetWars.DTOs
{
    public class ActionDto
    {
        public Guid StartingPlanet { get; set; }
        public Guid EndingPlanet { get; set; }
        public int NumberOfArmies { get; set; }
    }
}