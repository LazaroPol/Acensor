using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>
    /// Lo que el jugador lleva a una estación (GDD §17.3): su bolsa, lo que conoce y su nivel en cada
    /// oficio. Los oficios se crean al usarlos por primera vez con la curva indicada.
    /// </summary>
    public sealed class CraftingProfile
    {
        private readonly Dictionary<ProfessionId, ProfessionSkill> _skills = new Dictionary<ProfessionId, ProfessionSkill>();
        private readonly ProfessionCurve _curve;

        public CraftingProfile(IInventory bag, KnownRecipes knowledge = null, ProfessionCurve curve = null)
        {
            Bag = Guard.NotNull(bag, nameof(bag));
            Knowledge = knowledge ?? new KnownRecipes();
            _curve = curve ?? ProfessionCurve.Default;
        }

        public IInventory Bag { get; }
        public KnownRecipes Knowledge { get; }

        /// <summary>Oficios ya usados (para guardarlos o mostrarlos).</summary>
        public IReadOnlyCollection<ProfessionSkill> Skills => _skills.Values;

        public ProfessionSkill Skill(ProfessionId profession)
        {
            if (_skills.TryGetValue(profession, out var skill)) return skill;
            skill = new ProfessionSkill(profession, _curve);
            _skills.Add(profession, skill);
            return skill;
        }

        public int LevelOf(ProfessionId profession) => Skill(profession).Level;
    }
}
