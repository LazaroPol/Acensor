using Specimen.Core.Animation;
using Specimen.Core.Combat;
using Specimen.Core.Equipment;
using Specimen.Core.Stats;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// «Atacar» (F o clic derecho sobre un enemigo): ataque automático con el arma equipada, como en WoW. Golpea al
    /// objetivo una vez cada <see cref="WeaponSpec.Speed"/> segundos mientras esté a su alcance; el daño es el del
    /// arma más el poder de ataque. Varitas: proyectil a distancia.
    /// </summary>
    public sealed class AutoAttack : MonoBehaviour
    {
        private const float HitDelay = 0.25f;
        private const float RangeWarningEvery = 2f;

        private SandboxPlayer _player;
        private SandboxContext _context;
        private PlayerTarget _target;
        private float _swing;
        private float _pending = -1f;
        private float _rangeWarning;

        public bool Active { get; private set; }
        public WeaponSpec Weapon => _player.Kit != null ? _player.Gear.Weapon : WeaponSpec.Unarmed;
        public float SwingFraction => Weapon.Speed > 0f ? Mathf.Clamp01(_swing / Weapon.Speed) : 0f;

        public void Init(SandboxPlayer player, SandboxContext context, PlayerTarget target)
        {
            _player = player;
            _context = context;
            _target = target;
        }

        /// <summary>Empieza a atacar al objetivo (lo pide WoW: sin objetivo no hay ataque).</summary>
        public void Begin()
        {
            if (!_target.HasTarget)
            {
                _context.Events.Fail("No tienes objetivo (Tab o clic sobre un enemigo)");
                return;
            }
            Active = true;
        }

        public void Stop() => Active = false;

        public float RangeTo(SandboxEnemy enemy) => Weapon.Range + enemy.Radius;

        public float DistanceTo(SandboxEnemy enemy)
        {
            var offset = enemy.transform.position - transform.position;
            offset.y = 0f;
            return offset.magnitude;
        }

        public void Face(SandboxEnemy enemy)
        {
            var offset = enemy.transform.position - transform.position;
            offset.y = 0f;
            if (offset.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(offset);
        }

        private void Update()
        {
            if (_context.Input.AutoAttack.WasPressedThisFrame() && !UI.UiRoot.PointerOverUi())
            {
                bool key = UnityEngine.InputSystem.Keyboard.current?.fKey.wasPressedThisFrame ?? false;
                if (Active && key) Stop();
                else Begin();
            }
            if (_swing > 0f) _swing -= Time.deltaTime;
            _rangeWarning -= Time.deltaTime;
            TickPending();
            if (!Active || !_player.IsAlive) return;
            if (!_target.HasTarget)
            {
                Active = false;
                return;
            }
            if (_swing > 0f || _pending >= 0f) return;
            var enemy = _target.Current;
            if (DistanceTo(enemy) > RangeTo(enemy))
            {
                if (_rangeWarning <= 0f) _context.Events.Fail("Fuera de alcance");
                _rangeWarning = RangeWarningEvery;
                return;
            }
            Face(enemy);
            _player.Animator.Play(Weapon.IsRanged ? AnimSlot.Hechizo : AnimSlot.AtaqueMele);
            _swing = Weapon.Speed;
            _pending = HitDelay;
        }

        private void TickPending()
        {
            if (_pending < 0f) return;
            _pending -= Time.deltaTime;
            if (_pending >= 0f) return;
            var enemy = _target.Current;
            if (enemy == null || enemy.IsDead) return;
            var hit = new Hit(Weapon.Roll(_player.Stats.ValueOf(StatType.Damage), _context.Random), DamageType.Physical);
            if (!Weapon.IsRanged)
            {
                _context.Hits.Strike(enemy, hit, HitSource.Arma);
                return;
            }
            Projectile.Launch(transform.position + Vector3.up * 1.2f + transform.forward * 0.5f, transform.forward, 18f,
                Weapon.Range + 5f, 0.3f, new Color(0.8f, 0.7f, 1f), _context.Enemies,
                e => _context.Hits.Strike(e, hit, HitSource.Arma), enemy);
        }
    }
}
