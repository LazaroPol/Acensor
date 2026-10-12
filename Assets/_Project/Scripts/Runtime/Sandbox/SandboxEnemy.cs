using System;
using Specimen.Core.Animation;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Sandbox;
using Specimen.Runtime.Characters;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Criatura del bestiario en escena: la IA del núcleo decide (esperar/perseguir/atacar), aquí solo
    /// se mueve, se anima y aplica el golpe al jugador tras un breve amago.
    /// </summary>
    public sealed partial class SandboxEnemy : MonoBehaviour
    {
        private const float WindUp = 0.45f;
        private const float ReachTolerance = 1.4f;

        private Enemy _enemy;
        private EnemyHealth _health;
        private EnemyBrain _brain;
        private CharacterAnimator _animator;
        private SandboxPlayer _target;
        private Action<SandboxEnemy> _onDeath;
        private Vector3 _home;
        private Quaternion _homeRotation;
        private float _windUp = -1f;

        public BestiaryEntry Entry { get; private set; }
        public string DisplayName => Entry.DisplayName;
        public bool IsDead => _health == null || _health.IsDead;
        public float HealthFraction => _health?.Fraction ?? 0f;
        public float Radius => 0.45f * Entry.Scale;
        public float Height => 1.5f * Entry.Scale;

        public void Init(BestiaryEntry entry, Enemy enemy, SandboxPlayer target, Action<SandboxEnemy> onDeath,
            RuntimeAnimatorController controller)
        {
            Entry = entry;
            _enemy = enemy;
            _target = target;
            _onDeath = onDeath;
            SetupLevel(entry, enemy);
            _respawn = new RespawnTimer(SandboxRespawn.DelayFor(entry));
            _brain = new EnemyBrain(new EnemyBrainConfig().ScaledBy(entry.Scale));
            _animator = GetComponent<CharacterAnimator>();
            if (_animator == null) _animator = gameObject.AddComponent<CharacterAnimator>();
            _animator.Rebind(controller);
            _home = transform.position;
            _homeRotation = transform.rotation;
        }

        public void TakeHit(DamageResult result)
        {
            if (IsDead) return;
            _animator.Play(AnimSlot.Golpe);
            if (_health.TakeDamage(result.Final)) Die();
        }

        public void ResetToHome()
        {
            _health.Revive();
            _respawn.Cancel();
            _windUp = -1f;
            transform.SetPositionAndRotation(_home, _homeRotation);
            _animator.SetDead(false);
        }

        private void Die()
        {
            _animator.SetDead(true);
            _animator.SetSpeed(0f);
            _respawn.Begin();
            _onDeath?.Invoke(this);
        }

        private void Update()
        {
            if (_enemy == null) return;
            float dt = Time.deltaTime;
            if (IsDead)
            {
                TickRespawn(dt);
                return;
            }
            var flat = _target.transform.position - transform.position;
            flat.y = 0f;
            float distance = flat.magnitude;
            var action = _brain.Decide(distance, true, _target.IsAlive, dt);
            float speed = 0f;
            if (action == EnemyAction.Atacar) StartAttack();
            else if (action == EnemyAction.Perseguir && _windUp < 0f)
            {
                speed = Mathf.Min(_brain.Config.MoveSpeed, distance / Mathf.Max(dt, 1e-4f));
                transform.position += flat.normalized * (speed * dt);
            }
            if (_brain.IsEngaged && flat.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(flat);
            TickWindUp(dt, distance);
            _animator.SetSpeed(speed);
        }

        private void StartAttack()
        {
            _windUp = WindUp;
            _animator.Play(AnimSlot.AtaqueMele);
        }

        private void TickWindUp(float dt, float distance)
        {
            if (_windUp < 0f) return;
            _windUp -= dt;
            if (_windUp >= 0f) return;
            if (distance <= _brain.Config.AttackRange * ReachTolerance) _target.ReceiveHit(Attack(), DisplayName, Level);
        }
    }
}
