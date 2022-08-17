using PlanetWars.Communication;


namespace PlanetWars.DTOs.Communication
{
    public class NewPlayerDto: CommunicationParam
    {
        public string SessionID { get; set; }
        public PlayerDto NewPlayer { get; set; }
    }
}