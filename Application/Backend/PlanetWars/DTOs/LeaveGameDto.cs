using System;

namespace PlanetWars.DTOs
{
    public class LeaveGameDto
    {
        public Guid SessionID { get; set; }
        public Guid PlayerID { get; set; }
    }
}