using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Communication;


namespace PlanetWars.DTOs.Communication
{
    public class GameUpdateDto : CommunicationParam
    {
        public SessionDto Session { get; set; }
        public int ArmiesNextTurn { get; set; }
    }
}