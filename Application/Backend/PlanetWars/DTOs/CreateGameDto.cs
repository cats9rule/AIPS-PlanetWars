using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class CreateGameDto
    {
        public Guid ID { get; set; }

        public Guid UserId { get; set; }    //potrebno za kreiranje igraca

        // public int PlanetCount { get; set; }    //potrebno za kreiranje galaksije

        // public float ResourcePlanetRatio { get; set; }  //potrebno za kreiranje galaksije

        public Guid GameMapID { get; set; }

        public string Name { get; set; }
        
        public string Password { get; set; }

        public int MaxPlayers { get; set; }
    }
}