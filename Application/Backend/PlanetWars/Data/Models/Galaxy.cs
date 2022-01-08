using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlanetWars.Data.Models
{
    [Table("Galaxy")]
    public class Galaxy
    {
        [Key]
        public Guid ID { get; set; }

        [JsonIgnore]
        public virtual List<Planet> Planets { get; set; }
    }
}