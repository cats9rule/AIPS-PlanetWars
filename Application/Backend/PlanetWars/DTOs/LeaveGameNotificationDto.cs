using System;
using PlanetWars.DTOs.Communication;

namespace PlanetWars.DTOs
{
    public class LeaveGameNotificationDto
    {
        public GameUpdateDto GameUpdate { get; set; }
        public Guid PlayerID { get; set; }
        public string Message { get; set; }
    }
}