using System;
using Specimen.Core.Common;

namespace Specimen.Core.Abilities
{
    /// <summary>Habilidad equipada: comprueba enfriamiento, cobra el coste e inicia el enfriamiento.</summary>
    public sealed class AbilitySlot
    {
        private readonly IAbilityCostPayer _payer;

        public AbilitySlot(AbilityDefinition definition, IAbilityCostPayer payer)
        {
            Definition = Guard.NotNull(definition, nameof(definition));
            _payer = Guard.NotNull(payer, nameof(payer));
            Cooldown = new AbilityCooldown(definition.CooldownSeconds);
        }

        public event Action<AbilitySlot> Activated;

        public AbilityDefinition Definition { get; }
        public AbilityCooldown Cooldown { get; }
        public bool IsReady => Cooldown.IsReady;

        public AbilityResult TryActivate()
        {
            if (!Cooldown.IsReady) return AbilityResult.OnCooldown;
            var result = _payer.TryPay(Definition);
            if (result != AbilityResult.Activated) return result;
            Cooldown.Start();
            Activated?.Invoke(this);
            return result;
        }

        public void Tick(float deltaSeconds) => Cooldown.Tick(deltaSeconds);
    }
}
