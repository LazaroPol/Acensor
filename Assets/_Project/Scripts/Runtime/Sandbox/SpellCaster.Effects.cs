using Specimen.Core.Animation;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Attacks;
using Specimen.Core.Progression;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Efectos de cada tipo de hechizo en el Campo de pruebas.</summary>
    public sealed partial class SpellCaster
    {
        private const float MeleeRange = 2.8f;
        private const float ArcDegrees = 140f;

        private void Apply(SpellDefinition spell)
        {
            var hit = new Hit(Damage(spell), spell.Type);
            float bonus = 1f + _player.Bonuses.SpellPower;
            switch (spell.Kind)
            {
                case SpellKind.Golpe:
                    _player.Animator.Play(AnimSlot.AtaqueMele);
                    StrikeArc(hit);
                    break;
                case SpellKind.Proyectil:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    Projectile.Launch(transform.position + Vector3.up * 1.2f + transform.forward * 0.6f, transform.forward,
                        14f, 20f, 0.5f, DamageColors.Of(spell.Type), _context.Enemies, e => _context.Hits.Strike(e, hit));
                    break;
                case SpellKind.Area:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    StrikeAround(hit, spell.Radius);
                    break;
                case SpellKind.Curacion:
                    _player.Animator.Play(AnimSlot.Hechizo);
                    _player.Vitals.Health.Restore(spell.Power * bonus);
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

        private void StrikeArc(Hit hit)
        {
            var me = PlayerCombat.Planar(transform.position);
            float facing = transform.eulerAngles.y;
            foreach (var enemy in _context.Enemies.Alive())
                if (MeleeArc.Contains(me, facing, PlayerCombat.Planar(enemy.transform.position), MeleeRange, ArcDegrees,
                        enemy.Radius))
                    _context.Hits.Strike(enemy, hit);
        }

        private void StrikeAround(Hit hit, float radius)
        {
            foreach (var enemy in _context.Enemies.Alive())
            {
                var offset = enemy.transform.position - transform.position;
                offset.y = 0f;
                if (offset.magnitude <= radius + enemy.Radius) _context.Hits.Strike(enemy, hit);
            }
        }
    }
}
