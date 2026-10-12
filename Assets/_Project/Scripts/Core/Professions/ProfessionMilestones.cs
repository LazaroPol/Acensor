using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Professions
{
    /// <summary>Tabla de hitos de oficio (datos). Cada nivel aplica el último hito alcanzado.</summary>
    public sealed class ProfessionMilestones
    {
        private readonly ProfessionMilestone[] _steps;

        public ProfessionMilestones(IEnumerable<ProfessionMilestone> steps)
        {
            if (steps == null) throw new ArgumentNullException(nameof(steps));
            _steps = steps.ToArray();
            if (_steps.Length == 0 || _steps[0].Level != 1)
                throw new ArgumentException("Los hitos deben empezar en el nivel 1.", nameof(steps));
            for (int i = 1; i < _steps.Length; i++)
                if (_steps[i].Level <= _steps[i - 1].Level)
                    throw new ArgumentException("Los niveles de los hitos deben ser crecientes.", nameof(steps));
        }

        /// <summary>Tabla por defecto del GDD §17.1 (niveles 1, 5, 10, 15 y 20).</summary>
        public static ProfessionMilestones Default { get; } = new ProfessionMilestones(new[]
        {
            new ProfessionMilestone(1, 1, 0f, 0),
            new ProfessionMilestone(5, 2, 0.10f, 0),
            new ProfessionMilestone(10, 3, 0.20f, 1),
            new ProfessionMilestone(15, 4, 0.25f, 2),
            new ProfessionMilestone(20, 4, 0.30f, 3)
        });

        public IReadOnlyList<ProfessionMilestone> Steps => _steps;

        public ProfessionMilestone For(int level)
        {
            if (level < 1) throw new ArgumentOutOfRangeException(nameof(level));
            return _steps.Last(s => s.Level <= level);
        }
    }
}
