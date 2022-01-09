using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class SessionDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Creator { get; set; }

        public string PlayerOnTurn { get; set; }

        public List<string> Players { get; set; }

        public int Galaxy { get; set; }
    }
}