using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class PlayerDto
    {
        public string ID { get; set; }

        public string UserID { get; set; }

        public PlayerColorDto PlayerColor { get; set; }

        public virtual List<string> PlanetIDs { get; set; }

        public bool IsActive { get; set; }
    }
}