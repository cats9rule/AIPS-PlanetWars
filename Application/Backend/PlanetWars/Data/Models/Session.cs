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

        public int CurrentTurnIndex { get; set; }

        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }

        public int MaxPlayers { get; set; }

        public Galaxy Galaxy { get; set; }

        public int PlayerCount { get; set; }
    }
}