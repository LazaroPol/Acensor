using System;
using Specimen.Core.Common;

namespace Specimen.Core.Abilities
{
    /// <summary>Definición inmutable de una habilidad: coste de recurso de clase y de stamina, y enfriamiento.</summary>
    public sealed class AbilityDefinition
    {
        public AbilityDefinition(string id, string displayName, float resourceCost, float staminaCost,
            float cooldownSeconds)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Nombre vacío.", nameof(displayName));
            Id = id;
            DisplayName = displayName;
            ResourceCost = Guard.NonNegative(resourceCost, nameof(resourceCost));
            StaminaCost = Guard.NonNegative(staminaCost, nameof(staminaCost));
            CooldownSeconds = Guard.NonNegative(cooldownSeconds, nameof(cooldownSeconds));
        }

        public string Id { get; }
        public string DisplayName { get; }
        public float ResourceCost { get; }
        public float StaminaCost { get; }
        public float CooldownSeconds { get; }
    }
}
