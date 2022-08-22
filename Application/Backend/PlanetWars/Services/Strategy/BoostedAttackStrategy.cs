using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanetWars.Data.Models;
using PlanetWars.DTOs;
using PlanetWars.Services.Exceptions;

namespace PlanetWars.Services.Strategy
{
    public class BoostedAttackStrategy : IActionStrategy
    {
        public Session DoAction(ActionDto action, Session session, List<PlanetPlanet> connections)
        {
            ValidateAction(action, session, connections);
            session.Galaxy.Planets.Where(planet => planet.ID == action.PlanetFrom).FirstOrDefault().ArmyCount -= action.NumberOfArmies;
            var attackedPlanet = session.Galaxy.Planets.Where(planet => planet.ID == action.PlanetTo).FirstOrDefault();
            session.Galaxy.Planets.Remove(attackedPlanet);
            attackedPlanet.ArmyCount -= action.NumberOfArmies * 2;

            if (attackedPlanet.ArmyCount < 0)
            {
                attackedPlanet.OwnerID = action.PlayerID;
                attackedPlanet.ArmyCount *= -1;
            }

            session.Galaxy.Planets.Add(attackedPlanet);
            session.Galaxy.Planets.OrderBy(p => p.IndexInGalaxy);
            return session;
        }

        private bool ValidateAction(ActionDto action, Session session, List<PlanetPlanet> connections)
        {
            var planet1 = session.Players
                .Where(player => player.ID == action.PlayerID)
                .FirstOrDefault().Planets
                .Where(planet => planet.ID == action.PlanetFrom)
                .FirstOrDefault();
            if (planet1 != null) throw new InvalidActionException("Player does not own the start planet.");

            if (planet1.ArmyCount < action.NumberOfArmies)
                throw new InvalidActionException("Start planet does not have required number of armies.");

            var planet2 = session.Players
                .Where(player => player.ID == action.PlayerID)
                .FirstOrDefault().Planets
                .Where(planet => planet.ID == action.PlanetTo)
                .FirstOrDefault();
            if (planet2 != null) throw new InvalidActionException("Player does not own the destination planet.");

            if (connections.Where(c => c.PlanetFromID == planet1.ID && c.PlanetToID == planet2.ID).FirstOrDefault() == null)
                throw new InvalidActionException("Start and destination planet are not connected.");

            return true;
        }
    }
}