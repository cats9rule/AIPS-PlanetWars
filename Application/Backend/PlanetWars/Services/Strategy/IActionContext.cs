using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;

namespace PlanetWars.Services.Strategy
{
    public interface IActionContext
    {
        public void SetStrategy(IActionStrategy strategy);
        public Session DoAction(ActionDto action, Session session, List<PlanetPlanet> connections);
    }
}