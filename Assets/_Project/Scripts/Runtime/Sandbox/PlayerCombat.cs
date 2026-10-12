using Specimen.Core.Animation;
using Specimen.Core.Classes;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Attacks;
using Specimen.Core.Movement;
using Specimen.Core.Stats;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Ataque básico del kit (clic izquierdo / J): melé en arco delante del jugador o proyectil de hechizo.
    /// El golpe sale tras un pequeño retardo para coincidir con la animación.
    /// </summary>
    public sealed class PlayerCombat : MonoBehaviour
    {
        private const float MeleeDelay = 0.25f;
        private const float SpellDelay = 0.35f;

        private SandboxPlayer _player;
        private SandboxContext _context;
        private float _pending = -1f;

        public ClassAttack Attack { get; private set; }

        public void Init(SandboxPlayer player, SandboxContext context)
        {
            _player = player;
            _context = context;
            _player.Equipped += Rebuild;
        }

        private void OnDestroy()
        {
            if (_player != null) _player.Equipped -= Rebuild;
        }

        private void Rebuild(ClassKit kit)
        {
            var stats = _player.Stats;
            float speed = stats.Has(StatType.AttackSpeed) ? stats.ValueOf(StatType.AttackSpeed) : 1f;
            Attack = new ClassAttack(ClassAttackProfiles.For(kit.Id), _player.Vitals.ClassResource, speed);
            float crit = stats.Has(StatType.CritChance) ? Mathf.Clamp01(stats.ValueOf(StatType.CritChance)) : 0f;
            _context.Hits.Crit = new CritRules(crit);
            _pending = -1f;
        }

        private void Update()
        {
            if (Attack == null || !_player.IsAlive) return;
            Attack.Tick(Time.deltaTime);
            if (_context.Input.Attack.WasPressedThisFrame() && !SpellCaster.BlockedByUi()) TryAttack();
            if (_pending < 0f) return;
            _pending -= Time.deltaTime;
            if (_pending < 0f) Release();
        }

        private void TryAttack()
        {
            var result = Attack.TryStart();
            if (result == AttackStartResult.NoResource) _context.Feed.Add("Sin recurso de clase para el hechizo.");
            if (result != AttackStartResult.Started) return;
            bool melee = Attack.Profile.Style == AttackStyle.CuerpoACuerpo;
            _player.Animator.Play(melee ? AnimSlot.AtaqueMele : AnimSlot.Hechizo);
            _pending = melee ? MeleeDelay : SpellDelay;
        }

        private void Release()
        {
            var profile = Attack.Profile;
            var hit = profile.HitFor(_player.Stats.ValueOf(StatType.Damage));
            if (profile.Style == AttackStyle.Hechizo)
            {
                var origin = transform.position + Vector3.up * 1.2f + transform.forward * 0.6f;
                Projectile.Launch(origin, transform.forward, profile.ProjectileSpeed, profile.Range, 0.4f,
                    DamageColors.Of(profile.Type), _context.Enemies, enemy => _context.Hits.Strike(enemy, hit));
                return;
            }
            var me = Planar(transform.position);
            float facing = transform.eulerAngles.y;
            foreach (var enemy in _context.Enemies.Alive())
                if (MeleeArc.Contains(me, facing, Planar(enemy.transform.position), profile.Range, profile.ArcDegrees,
                        enemy.Radius))
                    _context.Hits.Strike(enemy, hit);
        }

        public static PlanarVector Planar(Vector3 v) => new PlanarVector(v.x, v.z);
    }
}
