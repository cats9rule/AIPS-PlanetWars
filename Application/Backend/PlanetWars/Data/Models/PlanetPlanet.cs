using System;

namespace PlanetWars.Data.Models
{
    public class PlanetPlanet
    {
        public Guid ID { get; set; }

        public Guid PlanetFromID { get; set; }

        public Guid PlanetToID { get; set; }

        public Guid SessionID { get; set; }
    }
}