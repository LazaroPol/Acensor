using Specimen.Core.Common;

namespace Specimen.Core.Combat.Attacks
{
    /// <summary>
    /// Ataque básico de un kit: estilo, tipo de daño, alcance y arco (melé) o velocidad del proyectil
    /// (hechizo), enfriamiento base y coste de recurso de clase. El daño sale de la stat Damage × multiplicador.
    /// </summary>
    public sealed class AttackProfile
    {
        public AttackProfile(AttackStyle style, DamageType type, float damageMultiplier, float range,
            float arcDegrees, float baseCooldown, float resourceCost = 0f, float projectileSpeed = 0f)
        {
            Style = style;
            Type = type;
            DamageMultiplier = Guard.Positive(damageMultiplier, nameof(damageMultiplier));
            Range = Guard.Positive(range, nameof(range));
            ArcDegrees = Guard.NonNegative(arcDegrees, nameof(arcDegrees));
            BaseCooldown = Guard.NonNegative(baseCooldown, nameof(baseCooldown));
            ResourceCost = Guard.NonNegative(resourceCost, nameof(resourceCost));
            ProjectileSpeed = Guard.NonNegative(projectileSpeed, nameof(projectileSpeed));
        }

        public AttackStyle Style { get; }
        public DamageType Type { get; }
        public float DamageMultiplier { get; }
        public float Range { get; }
        public float ArcDegrees { get; }
        public float BaseCooldown { get; }
        public float ResourceCost { get; }
        public float ProjectileSpeed { get; }

        public float DamageFor(float statDamage) => Guard.NonNegative(statDamage, nameof(statDamage)) * DamageMultiplier;

        /// <summary>Enfriamiento real: el base dividido por la velocidad de ataque del personaje.</summary>
        public float CooldownFor(float attackSpeed) => BaseCooldown / Guard.Positive(attackSpeed, nameof(attackSpeed));

        public Hit HitFor(float statDamage) => new Hit(DamageFor(statDamage), Type);
    }
}
