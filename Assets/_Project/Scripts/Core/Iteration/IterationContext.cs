using Specimen.Core.Common;
using Specimen.Core.Events;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration
{
    /// <summary>Estado compartido entre los estados de la iteración. Solo lo modifican los estados y IterationRun.</summary>
    public sealed class IterationContext
    {
        public IterationConfig Config { get; }
        public IEventPublisher Events { get; }

        public int Floor { get; internal set; }
        public PhaseTimer Timer { get; internal set; }
        public bool ArmoryCompleted { get; internal set; }
        public bool SiegeCompleted { get; internal set; }
        public bool ExtractionCompleted { get; internal set; }

        public bool IsLastFloor => Floor >= Config.FloorCount;

        public IterationContext(IterationConfig config, IEventPublisher events)
        {
            Config = Guard.NotNull(config, nameof(config));
            Events = Guard.NotNull(events, nameof(events));
        }
    }
}
