using System;

namespace PlanetWars.DTOs
{
    public class LeaveGameNotificationDto
    {
        public Guid SessionID { get; set; }
        public Guid PlayerID { get; set; }
        public string Message { get; set; }
    }
}