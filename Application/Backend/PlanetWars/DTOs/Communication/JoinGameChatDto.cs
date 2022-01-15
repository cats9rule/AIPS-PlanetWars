using System;
using PlanetWars.Communication;

namespace PlanetWars.DTOs
{
    public class JoinGameChatDto : CommunicationParam
    {
        public Guid SessionID { get; set; }
        public string UsernameWithTag { get; set; }
    }
}