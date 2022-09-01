using System;

namespace PlanetWars.DTOs
{
    public class PlanetDto
    {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public int ArmyCount { get; set; }
        public int IndexInGalaxy { get; set; }

        // public int MovementBoost { get; set; }
        // public int AttackBoost { get; set; }
        // public int DefenseBoost { get; set; }

        public string Extras { get; set; }

    }
}