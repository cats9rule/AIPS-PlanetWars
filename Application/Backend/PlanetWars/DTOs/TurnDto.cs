using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetWars.DTOs
{
    public class TurnDto
    {
        public List<ActionDto> Actions { get; set; }
        public Guid SessionID { get; set; }
        public Guid PlayerID { get; set; }
    }
}