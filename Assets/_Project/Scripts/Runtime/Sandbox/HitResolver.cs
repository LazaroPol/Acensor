using Specimen.Core.Combat;
using Specimen.Core.Randomness;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Resuelve golpes del jugador contra enemigos con el DamageCalculator del núcleo y lo anota en el registro.</summary>
    public sealed class HitResolver
    {
        private readonly DamageCalculator _calculator;
        private readonly CombatFeed _feed;

        public HitResolver(IRandom random, CombatFeed feed)
        {
            _calculator = new DamageCalculator(random);
            _feed = feed;
        }

        public CritRules Crit { get; set; } = CritRules.None;

        /// <summary>Bonificación de daño activa (Furia), como fracción: 0,25 = +25 %.</summary>
        public System.Func<float> DamageBonus { get; set; }

        public DamageResult Resolve(Hit hit, CritRules crit, DefenseProfile defense)
            => _calculator.Resolve(hit, crit, defense);

        public void Strike(SandboxEnemy enemy, Hit hit)
        {
            if (enemy == null || enemy.IsDead) return;
            float bonus = DamageBonus?.Invoke() ?? 0f;
            if (bonus > 0f) hit = new Hit(hit.Amount * (1f + bonus), hit.Type, hit.CanCrit);
            var result = _calculator.Resolve(hit, Crit, enemy.Defense);
            enemy.TakeHit(result);
            string crit = result.IsCritical ? " ¡crítico!" : string.Empty;
            _feed.Add($"{enemy.DisplayName}: -{result.Final:0} ({DamageColors.Name(result.Type)}){crit}");
        }
    }
}
