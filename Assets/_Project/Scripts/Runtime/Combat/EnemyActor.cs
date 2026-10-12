using Specimen.Core.Combat;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Vitals;
using UnityEngine;

namespace Specimen.Runtime.Combat
{
    /// <summary>Enemigo vivo en escena (esfera/cubo greybox): persigue al jugador y le pega con enfriamiento.</summary>
    public sealed class EnemyActor : MonoBehaviour
    {
        private Enemy _enemy;
        private EnemyBrain _brain;
        private Transform _target;
        private CharacterVitals _targetVitals;
        private EnemyHealth _health;

        public bool IsDead => _health.IsDead;
        public DefenseProfile Defense => _enemy.Defense;

        public void Init(Enemy enemy, Transform target, CharacterVitals targetVitals)
        {
            _enemy = enemy;
            _target = target;
            _targetVitals = targetVitals;
            _health = new EnemyHealth(enemy.Stats.MaxHealth);
            _brain = new EnemyBrain(new EnemyBrainConfig(attackRange: 1.8f, attackCooldown: 1.2f));
        }

        public void Hurt(float amount)
        {
            if (_health.TakeDamage(amount)) Destroy(gameObject);
        }

        private void Update()
        {
            if (_enemy == null || _target == null) return;
            var flat = _target.position - transform.position;
            flat.y = 0f;
            switch (_brain.Decide(flat.magnitude, !IsDead, _targetVitals.IsAlive, Time.deltaTime))
            {
                case EnemyAction.Perseguir:
                    transform.position += flat.normalized * (_brain.Config.MoveSpeed * Time.deltaTime);
                    break;
                case EnemyAction.Atacar:
                    var hit = _enemy.Attack();
                    _targetVitals.Health.Drain(hit.Amount);
                    break;
            }
        }
    }
}
