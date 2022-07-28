using System;

namespace PlanetWars.DTOs
{
    public class JoinGameDto
    {
        public Guid UserID { get; set; }
        public string SessionName { get; set; }
        public string GameCode { get; set; }

    }
}