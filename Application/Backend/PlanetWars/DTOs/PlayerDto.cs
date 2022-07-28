using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class PlayerDto
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public string Username { get; set; }

        public string PlayerColor { get; set; }

        public int TurnIndex { get; set; }

        public virtual List<Guid> PlanetIDs { get; set; }

        public bool IsActive { get; set; }

        public Guid SessionID { get; set; }
    }
}