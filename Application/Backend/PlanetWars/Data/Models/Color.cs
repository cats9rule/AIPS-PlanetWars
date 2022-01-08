using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetWars.Data.Models
{
    [Table("Color")]
    public class Color
    {
        [Key]
        public Guid ID { get; set; }
        public string HexValue { get; set; }
    }
}