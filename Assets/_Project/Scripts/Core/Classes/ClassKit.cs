using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Classes
{
    /// <summary>Definición inmutable de un kit de clase: rol, recurso, rasgos y stats base.</summary>
    public sealed class ClassKit
    {
        private readonly Dictionary<StatType, float> _baseStats;

        public ClassKit(ClassKitId id, string displayName, ClassRole role, ClassResourceType resource,
            ExplorationTraits traits, IReadOnlyDictionary<StatType, float> baseStats)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Nombre vacío.", nameof(displayName));
            Guard.NotNull(baseStats, nameof(baseStats));
            Id = id;
            DisplayName = displayName;
            Role = role;
            Resource = resource;
            Traits = traits;
            _baseStats = new Dictionary<StatType, float>();
            foreach (var pair in baseStats) _baseStats.Add(pair.Key, Guard.NonNegative(pair.Value, nameof(baseStats)));
        }

        public ClassKitId Id { get; }
        public string DisplayName { get; }
        public ClassRole Role { get; }
        public ClassResourceType Resource { get; }
        public ExplorationTraits Traits { get; }
        public IReadOnlyDictionary<StatType, float> BaseStats => _baseStats;

        public bool HasTrait(ExplorationTraits trait) => trait != ExplorationTraits.None && (Traits & trait) == trait;

        /// <summary>Crea una hoja de stats nueva con los valores base del kit.</summary>
        public StatSheet CreateStatSheet(IStatFormula formula = null)
        {
            var sheet = new StatSheet(formula);
            foreach (var pair in _baseStats) sheet.Define(pair.Key, pair.Value);
            return sheet;
        }
    }
}
