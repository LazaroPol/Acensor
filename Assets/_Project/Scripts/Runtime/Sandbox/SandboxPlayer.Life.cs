using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Muerte, reaparición e impulsos del jugador del Campo de pruebas.</summary>
    public sealed partial class SandboxPlayer
    {
        public void Revive(Vector3 position)
        {
            Vitals.RefillAll();
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
            _controller.enabled = false;
            _animator.SetDead(true);
            _context.Feed.Add("Has caído. Pulsa R para reaparecer (o F2 para curarte).");
        }
    }
}
