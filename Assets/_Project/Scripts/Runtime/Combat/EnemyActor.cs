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
        private CharacterController _body;
        private float _verticalSpeed;

        public event System.Action Died;

        public bool IsDead => _health.IsDead;
        public DefenseProfile Defense => _enemy.Defense;

        public void Init(Enemy enemy, Transform target, CharacterVitals targetVitals)
        {
            _enemy = enemy;
            _target = target;
            _targetVitals = targetVitals;
            _health = new EnemyHealth(enemy.Stats.MaxHealth);
            _brain = new EnemyBrain(new EnemyBrainConfig(attackRange: 1.8f, attackCooldown: 1.2f));
            _body = gameObject.AddComponent<CharacterController>();
            _body.radius = 0.5f;
            _body.height = 1f;
            _body.center = Vector3.zero;
        }

        public void Hurt(float amount)
        {
            if (!_health.TakeDamage(amount)) return;
            Died?.Invoke();
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_enemy == null || _target == null) return;
            var flat = _target.position - transform.position;
            flat.y = 0f;
            switch (_brain.Decide(flat.magnitude, !IsDead, _targetVitals.IsAlive, Time.deltaTime))
            {
                case EnemyAction.Perseguir:
                    Step(flat.normalized * _brain.Config.MoveSpeed);
                    break;
                case EnemyAction.Atacar:
                    var hit = _enemy.Attack();
                    _targetVitals.Health.Drain(hit.Amount);
                    break;
            }
        }

        /// <summary>Mueve con el CharacterController: respeta muros y cae al suelo.</summary>
        private void Step(Vector3 velocity)
        {
            _verticalSpeed = _body.isGrounded ? -1f : _verticalSpeed - 20f * Time.deltaTime;
            _body.Move((velocity + Vector3.up * _verticalSpeed) * Time.deltaTime);
        }
    }
}
