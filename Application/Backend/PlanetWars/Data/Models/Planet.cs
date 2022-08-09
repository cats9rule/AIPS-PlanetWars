using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlanetWars.Data.Models
{
    [Table("Planet")]
    public class Planet
    {
        [Key]
        public Guid ID { get; set; }

        public Guid? OwnerID { get; set; }

        [JsonIgnore]
        public Player Owner { get; set; }

        public Guid GalaxyID { get; set; }

        public Galaxy Galaxy { get; set; }
        public int IndexInGalaxy { get; set; }
        public int ArmyCount { get; set; }

        // [JsonIgnore]
        // public virtual List<PlanetPlanet> PlanetRelations { get; set; }

        // public int MovementBoost { get; set; }
        // public int AttackBoost { get; set; }
        // public int DefenseBoost { get; set; }

        public string Extras { get; set; }
    }
}