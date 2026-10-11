using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Estadísticas numéricas de un enemigo: vida máxima, daño por golpe y armadura.</summary>
    public sealed class EnemyStats
    {
        public EnemyStats(float maxHealth, float damage, float armor)
        {
            MaxHealth = Guard.Positive(maxHealth, nameof(maxHealth));
            Damage = Guard.Positive(damage, nameof(damage));
            Armor = Guard.NonNegative(armor, nameof(armor));
        }

        public float MaxHealth { get; }
        public float Damage { get; }
        public float Armor { get; }
    }
}
