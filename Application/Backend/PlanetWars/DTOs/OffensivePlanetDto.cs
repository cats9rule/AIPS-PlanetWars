
namespace PlanetWars.DTOs
{
    public class OffensivePlanetDto : PlanetDto
    {
        protected PlanetDto planet;

        public OffensivePlanetDto(PlanetDto planet)
        {
            this.planet = planet;
        }

        public int AttackBoost { get; set; }
    }
}