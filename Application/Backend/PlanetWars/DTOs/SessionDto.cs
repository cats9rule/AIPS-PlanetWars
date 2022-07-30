using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class SessionDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string GameCode { get; set; }

        public int CurrentTurnIndex { get; set; }

        public List<PlayerDto> Players { get; set; }
        
        public int PlayerCount { get; set; }

        public GalaxyDto Galaxy { get; set; }

        public int MaxPlayers { get; set; }
    }
}