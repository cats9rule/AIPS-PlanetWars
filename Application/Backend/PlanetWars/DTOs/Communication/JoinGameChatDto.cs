using System;
using PlanetWars.Communication;

namespace PlanetWars.DTOs
{
    //TODO: rename to JoinGameDto
    public class JoinGameChatDto : CommunicationParam
    {
        public string SessionID { get; set; }
        public string UsernameWithTag { get; set; }
    }
}