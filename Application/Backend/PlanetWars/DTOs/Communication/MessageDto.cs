using System;
using PlanetWars.Communication;

namespace PlanetWars.DTOs
{
    public class MessageDto : CommunicationParam
    {
        public Guid SessionID { get; set; }
        public string UsernameWithTag { get; set; }
        public string Contents { get; set; }
    }
}