using System;
using PlanetWars.Communication;

namespace PlanetWars.DTOs
{
    public class JoinSessionGroupDto : CommunicationParam
    {
        public string SessionID { get; set; }
        public string UsernameWithTag { get; set; }
    }
}