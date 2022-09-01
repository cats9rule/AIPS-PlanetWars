
namespace PlanetWars.DTOs
{
    public class ActionDto
    {
        public string PlayerID { get; set; }
        public string PlanetFrom { get; set; }
        public string PlanetTo { get; set; }
        public int Armies { get; set; }
        public string Type { get; set; }
    }
}