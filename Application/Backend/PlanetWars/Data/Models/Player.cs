using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlanetWars.Data.Models
{
    [Table("Player")]
    public class Player
    {
        [Key]
        public Guid ID { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public PlayerColor Color { get; set; }

        [JsonIgnore]
        public virtual List<Planet> Planets { get; set; }

        public bool IsActive { get; set; }
    }
}