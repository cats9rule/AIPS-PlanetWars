using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetWars.Data.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Tag { get; set; }
        public string DisplayedName { get; set; }    
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}