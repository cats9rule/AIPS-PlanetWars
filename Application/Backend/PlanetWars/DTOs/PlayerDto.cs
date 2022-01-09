using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class PlayerDto
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public string Color { get; set; }

        public int TurnIndex { get; set; }

        public virtual List<Guid> PlanetIDs { get; set; }

        public bool IsActive { get; set; }
    }
}