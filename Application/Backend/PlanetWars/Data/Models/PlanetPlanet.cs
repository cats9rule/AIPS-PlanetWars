using System;
using System.Text.Json.Serialization;

namespace PlanetWars.Data.Models
{
    public class PlanetPlanet
    {
        public Guid PlanetFromID { get; set; }

        [JsonIgnore]
        public Planet PlanetFrom { get; set; }
        
        public Guid PlanetToID { get; set; }

        [JsonIgnore]
        public Planet PlanetTo { get; set; }
    }
}