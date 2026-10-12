using Specimen.Core.Animation;
using Specimen.Core.Combat;
using Specimen.Core.Equipment;
using Specimen.Core.Progression;
using Specimen.Core.Stats;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Efectos de cada tipo de hechizo. Golpes y proyectiles necesitan objetivo (como en WoW): los golpes usan el
    /// daño del arma multiplicado por el hechizo y arrancan el ataque automático; los proyectiles persiguen al objetivo.
    /// </summary>
    public sealed partial class SpellCaster
    {
        private static bool NeedsTarget(SpellDefinition spell) => spell.Kind == SpellKind.Golpe || spell.Kind == SpellKind.Proyectil;

        private static float RangeOf(SpellDefinition spell) =>
            spell.Kind == SpellKind.Golpe ? WeaponSpec.MeleeRange : WeaponSpec.RangedRange;

        private bool TargetReady(SpellDefinition spell)
        {
            if (!NeedsTarget(spell) || _context.Hero.Level < spell.Level) return true;
            if (!_target.HasTarget)
            {
                _context.Events.Fail("No tienes objetivo (Tab o clic sobre un enemigo)");
                return false;
            }
            if (_attack.DistanceTo(_target.Current) <= RangeOf(spell) + _target.Current.Radius) return true;
            _context.Events.Fail("Fuera de alcance");
            return false;
        }

        private void Apply(SpellDefinition spell)
        {
            float bonus = 1f + _player.Bonuses.SpellPower;
            if (NeedsTarget(spell)) _attack.Face(_target.Current);
            switch (spell.Kind)
            {
                case SpellKind.Golpe:
                    _player.Animator.Play(AnimSlot.AtaqueMele);
                    float weapon = _attack.Weapon.Roll(_player.Stats.ValueOf(StatType.Damage), _context.Random);
                    _context.Hits.Strike(_target.Current, new Hit(weapon * spell.Power * bonus, spell.Type), HitSource.Habilidad);
                    _attack.Begin();
                    break;
                case SpellKind.Proyectil:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    var hit = new Hit(Damage(spell), spell.Type);
                    Projectile.Launch(transform.position + Vector3.up * 1.2f + transform.forward * 0.6f, transform.forward,
                        16f, WeaponSpec.RangedRange + 5f, 0.5f, DamageColors.Of(spell.Type), _context.Enemies,
                        e => _context.Hits.Strike(e, hit, HitSource.Habilidad), _target.Current);
                    break;
                case SpellKind.Area:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    StrikeAround(new Hit(Damage(spell), spell.Type), spell.Radius);
                    break;
                case SpellKind.Curacion:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    float healed = _player.Vitals.Health.Restore(spell.Power * bonus);
                    _context.Events.Heal(transform.position + Vector3.up * 2f, healed);
                    break;
                case SpellKind.Escudo:
                    _player.Animator.Play(AnimSlot.Interactuar);
                    _player.Shield(spell.Power, spell.Duration);
                    break;
                case SpellKind.Impulso:
                    // Convención de los datos: «Retrocede…» en la descripción = salto hacia atrás.
                    float sign = spell.Description.StartsWith("Retrocede") ? -1f : 1f;
                    _player.Dash(transform.forward * (spell.Power * sign));
                    break;
                default:
                    _player.Animator.Play(AnimSlot.Interactuar);
                    _context.Fury.Begin(spell.Power * bonus, spell.Duration);
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
