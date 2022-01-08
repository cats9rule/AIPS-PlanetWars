using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlanetWars.Data.Models
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public bool IsPrivate { get; set; }

        public string Password { get; set; }
        
        [JsonIgnore]
        public Player Creator { get; set; }

        [JsonIgnore]
        public Player PlayerOnTurn { get; set; }

        [JsonIgnore]
        public virtual List<Player> Players { get; set; }

        [JsonIgnore]
        public Galaxy Galaxy { get; set; }
    }
}