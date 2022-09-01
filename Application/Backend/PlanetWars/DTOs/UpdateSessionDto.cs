using System;

namespace PlanetWars.DTOs
{
    public class UpdateSessionDto
    {
        public Guid SessionID { get; set; }

        public bool NextPlayer { get; set; }

        public bool HasLostPlanet { get; set; }

        public bool HasWonPlanet { get; set; }

        public Guid PlanetID { get; set; }

        public Guid PlanetWinnerID { get; set; }

        public Guid PlanetLoserID { get; set; }
    }
}