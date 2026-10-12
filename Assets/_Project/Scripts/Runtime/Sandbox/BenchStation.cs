using System.Linq;
using Specimen.Core.Animation;
using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Banco de profesión: con E cerca se elige la profesión en el ProfessionBench y se ven sus sinergias.</summary>
    public sealed class BenchStation : MonoBehaviour
    {
        public const float UseRange = 2.4f;

        private SandboxContext _context;
        private SandboxPlayer _player;

        public ProfessionId Profession { get; private set; }

        public void Init(ProfessionId profession, SandboxContext context, SandboxPlayer player)
        {
            Profession = profession;
            _context = context;
            _player = player;
        }

        public bool IsPlayerNear()
        {
            if (_player == null) return false;
            var offset = _player.transform.position - transform.position;
            offset.y = 0f;
            return offset.magnitude <= UseRange;
        }

        private void Update()
        {
            if (!IsPlayerNear() || !_player.IsAlive || !_context.Input.Interact.WasPressedThisFrame()) return;
            _context.Bench.TryChoose(SandboxContext.LocalPlayer, Profession);
            _player.Animator.Play(AnimSlot.Interactuar);
            var recipes = DefaultProfessionSynergies.Create().Where(s => s.Involves(Profession))
                .Select(s => $"{s.First}+{s.Second}: {s.Recipe}").Take(3);
            _context.Feed.Add($"Profesión: {Profession}. Sinergias: {string.Join(" · ", recipes)}");
        }
    }
}
