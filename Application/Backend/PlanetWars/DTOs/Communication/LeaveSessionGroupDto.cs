using System;
using PlanetWars.Communication;

namespace PlanetWars.DTOs
{
    public class LeaveSessionGroupDto : CommunicationParam
    {
        public Guid SessionID { get; set; }
        public string UsernameWithTag { get; set; }
    }
}