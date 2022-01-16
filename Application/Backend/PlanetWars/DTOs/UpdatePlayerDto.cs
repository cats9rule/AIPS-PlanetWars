using System;

namespace PlanetWars.DTOs
{
    public class UpdatePlayerDto
    {
        public Guid PlayerID { get; set; }

        public bool ShouldAddPlanet { get; set; }

        public Guid AddPlanetID { get; set; }

        public bool ShouldRemovePlanet { get; set; }

        public Guid RemovePlayerID { get; set; }

        public bool ChangeActivity { get; set; }
    }
}