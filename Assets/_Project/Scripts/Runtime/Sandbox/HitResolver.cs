using System;
using Specimen.Core.Combat;
using Specimen.Core.Randomness;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Resuelve golpes con el DamageCalculator del núcleo y la armadura al estilo WoW (depende del nivel del atacante),
    /// lo anota en el registro y lo publica en <see cref="CombatEvents"/> para el texto flotante.
    /// </summary>
    public sealed class HitResolver
    {
        private readonly IRandom _random;
        private readonly CombatFeed _feed;
        private readonly CombatEvents _events;

        public HitResolver(IRandom random, CombatFeed feed, CombatEvents events)
        {
            _random = random;
            _feed = feed;
            _events = events;
        }

        public CritRules Crit { get; set; } = CritRules.None;

        /// <summary>Bonificación de daño activa (Furia), como fracción: 0,25 = +25 %.</summary>
        public Func<float> DamageBonus { get; set; }

        /// <summary>Nivel del jugador (la armadura del enemigo protege menos contra atacantes de más nivel).</summary>
        public Func<int> PlayerLevel { get; set; }

        public DamageResult Resolve(Hit hit, CritRules crit, DefenseProfile defense, int attackerLevel)
            => new DamageCalculator(_random, ArmorRules.MitigationFor(attackerLevel)).Resolve(hit, crit, defense);

        public void Strike(SandboxEnemy enemy, Hit hit, HitSource source)
        {
            if (enemy == null || enemy.IsDead) return;
            float bonus = DamageBonus?.Invoke() ?? 0f;
            if (bonus > 0f) hit = new Hit(hit.Amount * (1f + bonus), hit.Type, hit.CanCrit);
            var result = Resolve(hit, Crit, enemy.Defense, PlayerLevel?.Invoke() ?? 1);
            enemy.TakeHit(result);
            _events.Dealt(enemy, result, source);
            string crit = result.IsCritical ? " ¡crítico!" : string.Empty;
            _feed.Add($"{enemy.DisplayName}: -{result.Final:0} ({DamageColors.Name(result.Type)}){crit}");
        }
    }
}
