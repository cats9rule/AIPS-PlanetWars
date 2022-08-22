using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;
using PlanetWars.Services.Exceptions;


namespace PlanetWars.Services.Strategy
{
    public class PlacementStrategy : IActionStrategy
    {
        public PlacementStrategy() { }

        public Session DoAction(ActionDto action, Session session, List<PlanetPlanet> connections)
        {
            ValidateAction(action, session);
            session.Galaxy.Planets.Where(planet => planet.ID == action.PlanetTo).FirstOrDefault().ArmyCount += action.NumberOfArmies;
            return session;
        }

        private bool ValidateAction(ActionDto action, Session session)
        {
            var planet = session.Players
                .Where(player => player.ID == action.PlayerID)
                .FirstOrDefault().Planets
                .Where(planet => planet.ID == action.PlanetTo)
                .FirstOrDefault();
            if (planet != null) throw new InvalidActionException("Player does not own this planet.");

            return true;
        }
    }
}