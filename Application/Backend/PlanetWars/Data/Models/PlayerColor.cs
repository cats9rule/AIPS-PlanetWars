using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlanetWars.Data.Models
{
    [Table("PlayerColor")]
    public class PlayerColor
    {
        [Key]
        public Guid ID { get; set; }

        [JsonIgnore]
        public Color Color { get; set; }

        public int TurnIndex { get; set; }

    }
}