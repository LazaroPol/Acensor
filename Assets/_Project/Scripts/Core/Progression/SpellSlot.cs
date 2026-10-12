using System;
using Specimen.Core.Common;
using Specimen.Core.Vitals;

namespace Specimen.Core.Progression
{
    /// <summary>Hueco de la barra de acción para un hechizo: nivel requerido, coste del recurso y enfriamiento.</summary>
    public sealed class SpellSlot
    {
        public SpellSlot(SpellDefinition spell) => Spell = Guard.NotNull(spell, nameof(spell));

        public SpellDefinition Spell { get; }
        public float Remaining { get; private set; }
        public float LastCooldown { get; private set; }
        public float CooldownFraction => LastCooldown > 0f ? Remaining / LastCooldown : 0f;

        public void Tick(float dt) => Remaining = Math.Max(0f, Remaining - dt);

        public SpellCastResult Check(int level, ResourcePool resource)
        {
            if (level < Spell.Level) return SpellCastResult.NotLearned;
            if (Remaining > 0f) return SpellCastResult.OnCooldown;
            return resource == null || resource.Current >= Spell.Cost ? SpellCastResult.Cast : SpellCastResult.NoResource;
        }

        /// <param name="cooldownReduction">Fracción 0–0,5 que acorta el enfriamiento (talentos).</param>
        public SpellCastResult TryCast(int level, ResourcePool resource, float cooldownReduction = 0f)
        {
            var result = Check(level, resource);
            if (result != SpellCastResult.Cast) return result;
            if (Spell.Cost > 0f) resource?.TrySpend(Spell.Cost);
            LastCooldown = Spell.Cooldown * (1f - Math.Max(0f, Math.Min(0.5f, cooldownReduction)));
            Remaining = LastCooldown;
            return SpellCastResult.Cast;
        }

        public void Reset() => Remaining = 0f;
    }
}
