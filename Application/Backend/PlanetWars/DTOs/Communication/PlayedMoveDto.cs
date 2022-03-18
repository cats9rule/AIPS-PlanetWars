using PlanetWars.Communication;
using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class PlayedMoveDto : CommunicationParam
    {
        public ActionDto attackAction { get; set; }
        public ActionDto movementAction { get; set; }
        public Dictionary<Guid, int> ReinforcementsPlacement { get; set; }
        //key je Guid planete na koju su pojacanja postavljena, a int je broj pojacanja koji je postavljen na planetu
    }
}