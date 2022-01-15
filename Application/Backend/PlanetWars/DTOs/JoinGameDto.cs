using System;

namespace PlanetWars.DTOs
{
    public class JoinGameDto
    {
        public Guid UserID { get; set; }
        public Guid SessionID { get; set; }

    }
}