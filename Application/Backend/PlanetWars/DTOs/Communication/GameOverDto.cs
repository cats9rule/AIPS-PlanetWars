using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Communication;

namespace PlanetWars.DTOs.Communication
{
    public class GameOverDto : CommunicationParam
    {
        public string SessionID { get; set; }
        public PlayerDto winner { get; set; }
    }
}