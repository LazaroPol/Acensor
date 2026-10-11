using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Events;
using Specimen.Core.Iteration.Presence;
using Specimen.Core.Iteration.Readiness;
using Specimen.Core.Iteration.Voting;
using Specimen.Core.Time;

namespace Specimen.Core.Iteration
{
    /// <summary>Estado compartido entre los estados de la iteración. Solo lo modifican los estados y IterationRun.</summary>
    public sealed class IterationContext
    {
        public IterationConfig Config { get; }
        public IEventPublisher Events { get; }
        public ReadyCheck Ready { get; }
        public IHasteBonusPolicy HastePolicy { get; }
        public EarlyCloseVote EarlyClose { get; }
        public PresenceTracker Presence { get; }

        /// <summary>Bonus Prisa ganado por piso (piso → fracción extra de loot).</summary>
        public Dictionary<int, float> HasteBonuses { get; } = new Dictionary<int, float>();

        public int Floor { get; internal set; }
        public PhaseTimer Timer { get; internal set; }
        public bool ArmoryCompleted { get; internal set; }
        public bool SiegeCompleted { get; internal set; }
        public bool ExtractionCompleted { get; internal set; }

        public bool IsLastFloor => Floor >= Config.FloorCount;

        public IterationContext(IterationConfig config, IEventPublisher events, ReadyCheck ready, IHasteBonusPolicy hastePolicy)
        {
            Config = Guard.NotNull(config, nameof(config));
            Events = Guard.NotNull(events, nameof(events));
            Ready = Guard.NotNull(ready, nameof(ready));
            HastePolicy = Guard.NotNull(hastePolicy, nameof(hastePolicy));
            EarlyClose = new EarlyCloseVote(new MajorityVote(ready.Players));
            Presence = new PresenceTracker(ready.Players);
            if (ready.Players.Count > config.MaxPlayers)
                throw new ArgumentException($"Máximo {config.MaxPlayers} jugadores.", nameof(ready));
        }
    }
}
