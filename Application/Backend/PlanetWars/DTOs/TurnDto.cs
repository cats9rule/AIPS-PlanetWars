using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class TurnDto
    {
        public List<ActionDto> Actions { get; set; }
        public string SessionID { get; set; }
        public string PlayerID { get; set; }
    }
}