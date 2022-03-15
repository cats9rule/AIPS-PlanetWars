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

        public Guid SessionID { get; set; }

        [JsonIgnore]
        public Session Session { get; set; }

        public Guid UserID { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public Guid PlayerColorID { get; set; }
        
        [JsonIgnore]
        public PlayerColor PlayerColor { get; set; }

        [JsonIgnore]
        public virtual ICollection<Planet> Planets { get; set; }

        public bool IsActive { get; set; }

        public bool IsSessionOwner { get; set; }

        public int TotalArmies { get; set; }
    }
}