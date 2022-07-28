using System;
using System.Collections.Generic;

namespace PlanetWars.DTOs
{
    public class CreateGameDto
    {
        public Guid ID { get; set; }

        public Guid CreatorID { get; set; }    //potrebno za kreiranje igraca

        public Guid GameMapID { get; set; }

        public string Name { get; set; }

        public int MaxPlayers { get; set; }
    }
}