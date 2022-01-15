using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class SessionDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Guid Creator { get; set; }

        public Guid PlayerOnTurn { get; set; }

        public List<Guid> Players { get; set; }

        public Guid Galaxy { get; set; }

        public int MaxPlayers { get; set; }
    }
}