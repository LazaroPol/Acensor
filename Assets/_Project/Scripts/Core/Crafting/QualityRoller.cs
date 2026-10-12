using System;
using Specimen.Core.Items;
using Specimen.Core.Randomness;

namespace Specimen.Core.Crafting
{
    /// <summary>Tirada de calidad del GDD §17.1: puntos → grado, limitado por hito y receta.</summary>
    public sealed class QualityRoller : IQualityRoller
    {
        private readonly QualityRules _rules;
        private readonly IRandom _random;

        public QualityRoller(QualityRules rules, IRandom random)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public Quality Roll(QualityRollInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            var w = _rules.Weights;
            var score = w.BaseScore(input) + _random.Range(w.RandomMin, w.RandomMax + 1);
            var grade = (int)_rules.FromScore(score);
            var cap = Math.Min(Math.Min(input.MaxQuality, (int)input.RecipeCap), (int)Quality.Legendary);
            return (Quality)Math.Max((int)Quality.Crude, Math.Min(grade, cap));
        }
    }
}
