using System;
using System.Collections.Generic;
using System.Linq;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;
using PlanetWars.Services.Exceptions;

namespace PlanetWars.Services.Strategy
{
    public class BoostedMoveStrategy : IActionStrategy
    {
        public Session DoAction(ActionDto action, Session session, List<PlanetPlanet> connections)
        {
            ValidateAction(action, session, connections);
            session.Galaxy.Planets.Where(planet => planet.ID.ToString() == action.PlanetFrom).FirstOrDefault().ArmyCount -= action.Armies;
            session.Galaxy.Planets.Where(planet => planet.ID.ToString() == action.PlanetTo).FirstOrDefault().ArmyCount += action.Armies;
            return session;
        }

        private bool ValidateAction(ActionDto action, Session session, List<PlanetPlanet> connections)
        {
            var planet1 = session.Players
                .Where(player => player.ID.ToString() == action.PlayerID)
                .FirstOrDefault().Planets
                .Where(planet => planet.ID.ToString() == action.PlanetFrom)
                .FirstOrDefault();
            if (planet1 == null) throw new InvalidActionException("Player does not own the start planet.");

            if (planet1.ArmyCount < action.Armies)
                throw new InvalidActionException("Start planet does not have required number of armies.");

            var planet2 = session.Players
                .Where(player => player.ID.ToString() == action.PlayerID)
                .FirstOrDefault().Planets
                .Where(planet => planet.ID.ToString() == action.PlanetTo)
                .FirstOrDefault();
            if (planet2 == null) throw new InvalidActionException("Player does not own the destination planet.");


            if (connections.Where(c => c.PlanetFromID == planet1.ID && c.PlanetToID == planet2.ID).FirstOrDefault() == null)
            {
                bool foundPlanet = false;
                List<PlanetPlanet> extendedConnections = new List<PlanetPlanet>();
                connections.Where(c => c.PlanetFromID == planet1.ID).ToList().ForEach(c =>
                {
                    Guid newPlanetFromID = c.PlanetToID;
                    if (connections.Where(c => c.PlanetFromID == newPlanetFromID && c.PlanetToID == planet2.ID) != null)
                    {
                        foundPlanet = true;
                    }
                });
                if (!foundPlanet) throw new InvalidActionException("Start and destination planet are not connected.");
            }

            return true;
        }
    }
}