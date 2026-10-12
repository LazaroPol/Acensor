using System.Collections.Generic;
using Specimen.Core.Animation;
using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Elige el interactuable más cercano al jugador (para el aviso «E · …») y lo usa al pulsar E.</summary>
    public sealed class InteractionHub : MonoBehaviour
    {
        private readonly List<IInteractable> _targets = new List<IInteractable>();
        private SandboxContext _context;
        private SandboxPlayer _player;

        public IInteractable Current { get; private set; }

        public void Init(SandboxContext context, SandboxPlayer player)
        {
            _context = context;
            _player = player;
            context.Interactions = this;
        }

        public void Register(IInteractable target) => _targets.Add(target);

        private void Update()
        {
            Current = _player != null && _player.IsAlive ? Nearest() : null;
            if (Current == null || !_context.Input.Interact.WasPressedThisFrame()) return;
            _player.Animator.Play(AnimSlot.Interactuar);
            UiTooltip.Hide();
            Current.Interact(_player);
        }

        private IInteractable Nearest()
        {
            var position = _player.transform.position;
            IInteractable best = null;
            float bestDistance = float.MaxValue;
            foreach (var target in _targets)
            {
                if (target == null || !target.CanInteract) continue;
                var offset = target.Position - position;
                offset.y = 0f;
                float distance = offset.magnitude;
                if (distance > target.Range || distance >= bestDistance) continue;
                best = target;
                bestDistance = distance;
            }
            return best;
        }
    }
}
