using Specimen.Core.Abilities;
using Specimen.Core.Animation;
using Specimen.Core.Classes;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Attacks;
using Specimen.Core.Sandbox;
using Specimen.Core.Stats;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Habilidad de firma del kit (Q): cobra con AbilitySlot del núcleo y aplica su SignatureEffect.</summary>
    public sealed class SignatureCaster : MonoBehaviour
    {
        private SandboxPlayer _player;
        private SandboxContext _context;
        private SignatureEffect _effect;
        private DamageType _type;

        public AbilitySlot Slot { get; private set; }

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
            Slot = new AbilitySlot(DefaultClassAbilities.SignatureOf(kit.Id), PoolCostPayer.From(_player.Vitals));
            _effect = SignatureEffects.For(kit.Id);
            _type = ClassAttackProfiles.For(kit.Id).Type;
        }

        private void Update()
        {
            if (Slot == null || !_player.IsAlive) return;
            Slot.Tick(Time.deltaTime);
            if (!_context.Input.Ability.WasPressedThisFrame()) return;
            var result = Slot.TryActivate();
            if (result != AbilityResult.Activated)
            {
                _context.Events.Fail(result == AbilityResult.OnCooldown
                    ? $"{Slot.Definition.DisplayName}: en enfriamiento ({Slot.Cooldown.Remaining:0.0} s)"
                    : $"{Slot.Definition.DisplayName}: recurso insuficiente");
                return;
            }
            _context.Feed.Add($"¡{Slot.Definition.DisplayName}!");
            Apply();
        }

        private void Apply()
        {
            var hit = new Hit(_player.Stats.ValueOf(StatType.Damage) * _effect.Magnitude, _type);
            switch (_effect.Kind)
            {
                case SignatureKind.Escudo:
                    _player.Shield(_effect.Magnitude, _effect.Duration);
                    _player.Animator.Play(AnimSlot.Interactuar);
                    break;
                case SignatureKind.Impulso:
                    _player.Dash(transform.forward * _effect.Magnitude);
                    break;
                case SignatureKind.Curacion:
                    _context.Events.Heal(transform.position + Vector3.up * 2f, _player.Vitals.Health.Restore(_effect.Magnitude));
                    _player.Animator.Play(AnimSlot.Hechizo);
                    break;
                case SignatureKind.Orbe:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    Projectile.Launch(transform.position + Vector3.up * 1.2f + transform.forward * 0.6f, transform.forward,
                        10f, 18f, _effect.Radius, DamageColors.Of(_type), _context.Enemies, e => _context.Hits.Strike(e, hit, HitSource.Habilidad),
                        GetComponent<PlayerTarget>()?.Current);
                    break;
                default:
                    _player.Animator.Play(_effect.Kind == SignatureKind.Area ? AnimSlot.Hechizo : AnimSlot.AtaqueMele);
                    StrikeAround(hit, _effect.Radius);
                    break;
            }
        }

        private void StrikeAround(Hit hit, float radius)
        {
            foreach (var enemy in _context.Enemies.Alive())
            {
                var offset = enemy.transform.position - transform.position;
                offset.y = 0f;
                if (offset.magnitude <= radius + enemy.Radius) _context.Hits.Strike(enemy, hit, HitSource.Habilidad);
            }
        }
    }
}
