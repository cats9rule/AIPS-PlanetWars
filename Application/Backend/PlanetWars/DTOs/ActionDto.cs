using System;

namespace PlanetWars.DTOs
{
    public class ActionDto
    {
        public Guid PlayerID { get; set; }
        public Guid PlanetFrom { get; set; }
        public Guid PlanetTo { get; set; }
        public int NumberOfArmies { get; set; }
        public string Type { get; set; }
    }
}