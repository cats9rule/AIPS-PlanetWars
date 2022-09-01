using PlanetWars.Communication;

namespace PlanetWars.DTOs
{
    public class MessageDto : CommunicationParam
    {
        public string SessionID { get; set; }
        public string UsernameWithTag { get; set; }
        public string Contents { get; set; }
    }
}