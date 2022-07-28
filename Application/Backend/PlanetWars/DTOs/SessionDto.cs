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

        public List<Guid> PlayerIDs { get; set; }
        
        public int PlayerCount { get; set; }

        public Guid GalaxyID { get; set; }

        public int MaxPlayers { get; set; }
    }
}