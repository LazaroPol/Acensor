using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Echoes
{
    /// <summary>Registro de los Ecos ya decididos en la iteración. Cada Eco se decide una sola vez.</summary>
    public sealed class EchoRegistry
    {
        private readonly List<EchoDecision> _decisions = new List<EchoDecision>();
        private readonly Dictionary<string, EchoDecision> _byId = new Dictionary<string, EchoDecision>();

        public int Count => _decisions.Count;

        /// <summary>Todas las decisiones, en el orden en que se tomaron.</summary>
        public IReadOnlyList<EchoDecision> Decisions => _decisions;

        public void Record(EchoDecision decision)
        {
            Guard.NotNull(decision, nameof(decision));
            if (_byId.ContainsKey(decision.EchoId))
                throw new InvalidOperationException($"El Eco '{decision.EchoId}' ya está decidido.");
            _byId.Add(decision.EchoId, decision);
            _decisions.Add(decision);
        }

        public bool IsDecided(string echoId) => echoId != null && _byId.ContainsKey(echoId);

        /// <summary>Opción elegida para el Eco, o null si aún no se decidió.</summary>
        public int? ChosenOption(string echoId) => IsDecided(echoId) ? _byId[echoId].Option : (int?)null;

        public bool Chose(string echoId, int option) => ChosenOption(echoId) == option;

        /// <summary>Decisiones tomadas en el piso indicado.</summary>
        public IReadOnlyList<EchoDecision> DecidedOnFloor(int floor)
            => _decisions.Where(d => d.Floor == floor).ToList();

        public void Clear()
        {
            _decisions.Clear();
            _byId.Clear();
        }
    }
}
