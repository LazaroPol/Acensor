using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Echoes.Secrets
{
    /// <summary>Decisiones secretas e individuales: cada jugador decide una vez por Eco y el resto las ve al revelarse.</summary>
    public sealed class SecretEchoLedger
    {
        private readonly List<SecretDecision> _decisions = new List<SecretDecision>();

        public int Count => _decisions.Count;

        public void Record(SecretDecision decision)
        {
            Guard.NotNull(decision, nameof(decision));
            if (Find(decision.Voter, decision.EchoId) != null)
                throw new InvalidOperationException($"El jugador {decision.Voter} ya decidió '{decision.EchoId}'.");
            _decisions.Add(decision);
        }

        /// <summary>Opción que <paramref name="viewer"/> puede ver de la decisión de <paramref name="voter"/>, o null si sigue oculta.</summary>
        public int? ChoiceVisibleTo(int viewer, int voter, string echoId, int currentFloor)
        {
            var d = Find(voter, echoId);
            if (d == null) return null;
            return viewer == voter || d.IsRevealedAt(currentFloor) ? d.Option : (int?)null;
        }

        /// <summary>Decisiones ya reveladas en el piso actual, en orden de registro.</summary>
        public IReadOnlyList<SecretDecision> RevealedAt(int currentFloor)
            => _decisions.Where(d => d.IsRevealedAt(currentFloor)).ToList();

        /// <summary>Decisiones que se revelan exactamente al llegar a este piso (para anunciarlas).</summary>
        public IReadOnlyList<SecretDecision> RevealedOn(int floor)
            => _decisions.Where(d => d.RevealFloor == floor).ToList();

        public void Clear() => _decisions.Clear();

        private SecretDecision Find(int voter, string echoId)
            => _decisions.FirstOrDefault(d => d.Voter == voter && d.EchoId == echoId);
    }
}
