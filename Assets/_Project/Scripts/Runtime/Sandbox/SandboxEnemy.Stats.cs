using Specimen.Core.Bestiary;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Sandbox;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Nivel de la criatura y sus stats escalados (vida, daño y armadura), como los «mobs» de WoW.</summary>
    public sealed partial class SandboxEnemy
    {
        public int Level { get; private set; }
        public DefenseProfile Defense { get; private set; }
        public float MaxHealth { get; private set; }
        public float CurrentHealth => _health != null ? _health.Fraction * MaxHealth : 0f;

        private void SetupLevel(BestiaryEntry entry, Enemy enemy)
        {
            Level = CreatureLevels.LevelOf(entry);
            MaxHealth = CreatureLevels.Health(enemy.Stats.MaxHealth, Level);
            _health = new EnemyHealth(MaxHealth);
            Defense = new DefenseProfile(CreatureLevels.Armor(enemy.Defense.Armor, Level), enemy.Defense.Resistances);
        }

        private Hit Attack()
        {
            var hit = _enemy.Attack();
            return new Hit(CreatureLevels.Damage(hit.Amount, Level), hit.Type, hit.CanCrit);
        }
    }
}
