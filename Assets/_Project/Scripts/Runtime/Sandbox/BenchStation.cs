using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Estación de oficio del poblado (GDD §17.7): envuelve una <see cref="StationSession"/> (estación, cola,
    /// combustible y piezas) que avanza en tiempo real; con E cerca abre su ventana.
    /// </summary>
    public sealed class BenchStation : MonoBehaviour, IInteractable
    {
        public const float UseRange = 2.6f;

        private SandboxContext _context;

        public ProfessionId Profession { get; private set; }
        public StationSession Session { get; private set; }
        public string Prompt => $"Usar {StationLabels.Station(Session.Station.Type).ToLowerInvariant()}";
        public Vector3 Position => transform.position;
        public float Range => UseRange;
        public bool CanInteract => isActiveAndEnabled;

        public void Init(ProfessionId profession, SandboxContext context)
        {
            Profession = profession;
            _context = context;
            Session = context.Stations.Create(StationTypes.MainOf(profession));
            Session.Completed += context.ReportCraft;
        }

        public void Interact(SandboxPlayer player) => _context.OpenBench(this);

        private void Update() => Session?.Tick(Time.deltaTime);

        private void OnDestroy()
        {
            if (Session != null && _context != null) Session.Completed -= _context.ReportCraft;
        }
    }
}
