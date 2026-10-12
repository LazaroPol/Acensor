using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Banco de profesión: con E cerca abre la ventana de recetas de su profesión (estilo WoW).</summary>
    public sealed class BenchStation : MonoBehaviour, IInteractable
    {
        public const float UseRange = 2.6f;

        private SandboxContext _context;

        public ProfessionId Profession { get; private set; }
        public string Prompt => $"Usar banco de {Profession}";
        public Vector3 Position => transform.position;
        public float Range => UseRange;
        public bool CanInteract => isActiveAndEnabled;

        public void Init(ProfessionId profession, SandboxContext context)
        {
            Profession = profession;
            _context = context;
        }

        public void Interact(SandboxPlayer player) => _context.OpenBench(Profession);
    }
}
