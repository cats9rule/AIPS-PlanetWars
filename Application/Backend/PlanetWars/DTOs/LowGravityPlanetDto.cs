
namespace PlanetWars.DTOs
{
    public class LowGravityPlanetDto : PlanetDto
    {
        protected PlanetDto planet;

        public LowGravityPlanetDto(PlanetDto planet)
        {
            this.planet = planet;
        }

        public int MovementBoost { get; set; }
    }
}