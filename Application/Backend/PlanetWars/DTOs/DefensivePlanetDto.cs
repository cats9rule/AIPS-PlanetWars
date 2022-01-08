
namespace PlanetWars.DTOs
{
    public class DefensivePlanetDto : PlanetDto
    {
        protected PlanetDto planet;

        public DefensivePlanetDto(PlanetDto planet)
        {
            this.planet = planet;
        }

        public int DefenseBoost { get; set; }
    }
}