using Specimen.Core.Combat;
using Specimen.Core.Randomness;
using Specimen.Core.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Specimen.Runtime.Combat
{
    /// <summary>Ataque cuerpo a cuerpo del jugador (clic izquierdo o F): golpea a los enemigos delante, en un radio corto.</summary>
    public sealed class PlayerAttack : MonoBehaviour
    {
        private const float Range = 2.5f;
        private const float Cooldown = 0.5f;
        private StatSheet _stats;
        private DamageCalculator _damage;
        private float _cooldownLeft;

        public void Init(StatSheet stats)
        {
            _stats = stats;
            _damage = new DamageCalculator(new Pcg32Random((ulong)System.Environment.TickCount));
        }

        private void Update()
        {
            _cooldownLeft -= Time.deltaTime;
            if (_stats == null || _cooldownLeft > 0f || !Pressed()) return;
            _cooldownLeft = Cooldown;
            var hit = new Hit(_stats.ValueOf(StatType.Damage), DamageType.Physical);
            var crit = new CritRules(_stats.Has(StatType.CritChance) ? _stats.ValueOf(StatType.CritChance) : 0f);
            foreach (var enemy in FindObjectsByType<EnemyActor>(FindObjectsSortMode.None))
            {
                var to = enemy.transform.position - transform.position;
                if (to.magnitude > Range || Vector3.Dot(transform.forward, to.normalized) < 0.2f) continue;
                enemy.Hurt(_damage.Resolve(hit, crit, enemy.Defense).Final);
            }
        }

        private static bool Pressed() =>
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            || (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame);
    }
}
