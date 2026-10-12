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
