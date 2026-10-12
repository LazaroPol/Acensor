using System;
using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Professions
{
    /// <summary>
    /// Un <see cref="ProfessionSkill"/> por cada <see cref="ProfessionId"/>: el nivel existe para todos
    /// los oficios, aprendidos o no, para no perder progreso al cambiar de oficio.
    /// </summary>
    public sealed class ProfessionLevels
    {
        private readonly Dictionary<ProfessionId, ProfessionSkill> _skills;

        public ProfessionLevels(ProfessionCurve curve = null)
        {
            var used = curve ?? ProfessionCurve.Default;
            _skills = Enum.GetValues(typeof(ProfessionId)).Cast<ProfessionId>()
                .ToDictionary(id => id, id => new ProfessionSkill(id, used));
        }

        public ProfessionSkill For(ProfessionId profession) => _skills[profession];

        public int LevelOf(ProfessionId profession) => For(profession).Level;

        public IEnumerable<ProfessionSkill> All => _skills.Values;
    }
}
