using PlanetWars.Communication;


namespace PlanetWars.DTOs.Communication
{
    public class GameUpdateDto : CommunicationParam
    {
        public SessionDto Session { get; set; }
        public int ArmiesNextTurn { get; set; }
    }
}