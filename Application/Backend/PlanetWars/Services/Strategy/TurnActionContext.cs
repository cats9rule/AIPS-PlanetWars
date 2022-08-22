using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;

namespace PlanetWars.Services.Strategy
{
    public class TurnActionContext : ITurnActionContext
    {
        private IActionStrategy _strategy;

        public TurnActionContext() { }

        public Session DoAction(ActionDto action, Session session, List<PlanetPlanet> connections)
        {
            return _strategy.DoAction(action, session, connections);
        }

        public void SetStrategy(IActionStrategy strategy)
        {
            _strategy = strategy;
        }


    }
}