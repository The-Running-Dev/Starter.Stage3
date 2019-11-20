using System;

namespace Starter.Data.Entities
{
    /// <summary>
    /// Implements the Cat entity
    /// </summary>
    public class Cat: Entity
    {
        public string Name { get; set; }

        public Ability AbilityId { get; set; }

        public Guid SecondaryId { get; set; }
    }
}