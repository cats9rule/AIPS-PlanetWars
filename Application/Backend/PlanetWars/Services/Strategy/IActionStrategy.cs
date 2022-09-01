using System.Collections.Generic;
using PlanetWars.DTOs;
using PlanetWars.Data.Models;


namespace PlanetWars.Services.Strategy
{
    public interface IActionStrategy
    {
        public Session DoAction(ActionDto action, Session session, List<PlanetPlanet> connections);
    }
}