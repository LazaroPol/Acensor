using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Muerte, reaparición e impulsos del jugador del Campo de pruebas.</summary>
    public sealed partial class SandboxPlayer
    {
        // La vida se regenera sola (PlayerController): sin esta bandera el personaje «revivía» un frame
        // después de llegar a 0 y nunca llegaba a reproducir la animación de muerte.
        private bool _dead;

        private void OnHealthDepleted(Core.Vitals.ResourcePool pool) => Die();

        /// <summary>Reaplica nivel y talentos a la hoja de stats (las barras crecen al momento); al subir de nivel, rellena.</summary>
        public void RefreshProgression(bool refill)
        {
            if (Kit == null) return;
            Bonuses = _context.Hero.Apply(Kit.Id, Stats);
            ApplyCrit();
            if (refill && !_dead) Vitals.RefillAll();
        }

        /// <summary>Equipo del kit actual (arma con su velocidad, armadura…).</summary>
        public Core.Equipment.CharacterEquipment Gear => _context.Hero.EquipmentFor(Kit.Id);

        private void ApplyCrit()
        {
            var crit = Core.Stats.StatType.CritChance;
            float chance = Stats.Has(crit) ? Mathf.Clamp01(Stats.ValueOf(crit)) : 0f;
            _context.Hits.Crit = new Core.Combat.CritRules(chance, 2f);
        }

        public void Revive(Vector3 position)
        {
            Vitals.RefillAll();
            _dead = false;
            _controller.enabled = true;
            _animator.SetDead(false);
            Teleport(position);
        }

        /// <summary>Desplazamiento inmediato respetando colisiones (habilidad de impulso).</summary>
        public void Dash(Vector3 offset)
        {
            var body = GetComponent<CharacterController>();
            if (body != null && body.enabled) body.Move(offset);
            _animator.Play(Core.Animation.AnimSlot.Esquiva);
        }

        public void Teleport(Vector3 position)
        {
            var body = GetComponent<CharacterController>();
            if (body != null) body.enabled = false;
            transform.position = position;
            _lastPosition = position;
            if (body != null) body.enabled = true;
        }

        private void Die()
        {
            if (_dead) return;
            _dead = true;
            _controller.enabled = false;
            _animator.SetDead(true);
            _context.Feed.Add("Has caído. Pulsa R para reaparecer (o F2 para curarte).");
        }
    }
}
