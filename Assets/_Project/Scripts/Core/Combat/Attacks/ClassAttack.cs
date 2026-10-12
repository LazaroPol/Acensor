using Specimen.Core.Abilities;
using Specimen.Core.Common;
using Specimen.Core.Vitals;

namespace Specimen.Core.Combat.Attacks
{
    /// <summary>Ataque básico en uso: respeta su enfriamiento y cobra el recurso de clase (hechizos).</summary>
    public sealed class ClassAttack
    {
        private readonly ResourcePool _resource;
        private readonly AbilityCooldown _cooldown;

        public ClassAttack(AttackProfile profile, ResourcePool resource, float attackSpeed)
        {
            Profile = Guard.NotNull(profile, nameof(profile));
            _resource = Guard.NotNull(resource, nameof(resource));
            _cooldown = new AbilityCooldown(profile.CooldownFor(attackSpeed));
        }

        public AttackProfile Profile { get; }
        public bool IsReady => _cooldown.IsReady;
        public float CooldownFraction => _cooldown.Fraction;

        public AttackStartResult TryStart()
        {
            if (!_cooldown.IsReady) return AttackStartResult.OnCooldown;
            if (Profile.ResourceCost > 0f && !_resource.TrySpend(Profile.ResourceCost))
                return AttackStartResult.NoResource;
            _cooldown.Start();
            return AttackStartResult.Started;
        }

        public void Tick(float deltaSeconds) => _cooldown.Tick(deltaSeconds);
    }
}
