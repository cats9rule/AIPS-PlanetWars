using System;

namespace PlanetWars.DTOs
{
    public class UserDto
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Tag { get; set; }
        public string DisplayedName { get; set; }    
        public string Password { get; set; }
    }
}