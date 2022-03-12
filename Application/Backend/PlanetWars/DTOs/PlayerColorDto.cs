using System;

namespace PlanetWars.DTOs
{
    public class PlayerColorDto
    {
        public Guid ID { get; set; }
        public string HexColor { get; set; }
        public string ColorName { get; set; }
        public int TurnIndex { get; set; }
    }
}