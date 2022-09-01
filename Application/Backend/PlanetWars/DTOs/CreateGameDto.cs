using System;

namespace PlanetWars.DTOs
{
    public class CreateGameDto
    {
        public Guid UserID { get; set; }    //potrebno za kreiranje igraca

        public Guid GameMapID { get; set; }

        public string Name { get; set; }

    }
}