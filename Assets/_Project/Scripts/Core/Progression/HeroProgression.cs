using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Core.Common;
using Specimen.Core.Professions;
using Specimen.Core.Stats;
using Specimen.Core.Talents;

namespace Specimen.Core.Progression
{
    /// <summary>
    /// Progresión del personaje: nivel y experiencia compartidos, un reparto de talentos por kit
    /// (cambiar de kit conserva los talentos de cada uno) y profesiones aprendidas.
    /// </summary>
    public sealed class HeroProgression
    {
        private readonly Dictionary<ClassKitId, TalentAllocation> _talents = new Dictionary<ClassKitId, TalentAllocation>();
        private readonly Dictionary<ClassKitId, Spellbook> _books = new Dictionary<ClassKitId, Spellbook>();

        public HeroProgression(CharacterProgress progress = null) => Progress = progress ?? new CharacterProgress();

        public CharacterProgress Progress { get; }
        public LearnedProfessions Professions { get; } = new LearnedProfessions();
        public int Level => Progress.Level;

        public TalentAllocation TalentsFor(ClassKitId kit)
        {
            if (!_talents.TryGetValue(kit, out var allocation))
                _talents[kit] = allocation = new TalentAllocation(KitTalentTrees.For(kit));
            return allocation;
        }

        public Spellbook SpellbookFor(ClassKitId kit)
        {
            if (!_books.TryGetValue(kit, out var book)) _books[kit] = book = KitSpellbooks.For(kit);
            return book;
        }

        public int AvailablePoints(ClassKitId kit) => Progress.TalentPoints - TalentsFor(kit).TotalSpent;

        public bool CanSpend(ClassKitId kit, string talentId) => TalentsFor(kit).CanSpend(talentId, Progress.TalentPoints);

        public bool TrySpend(ClassKitId kit, string talentId) => TalentsFor(kit).TrySpend(talentId, Progress.TalentPoints);

        /// <summary>Aplica nivel y talentos del kit a su hoja de stats (idempotente) y devuelve los bonos no-stat.</summary>
        public TalentBonuses Apply(ClassKitId kit, StatSheet sheet)
        {
            Guard.NotNull(sheet, nameof(sheet));
            LevelStats.Apply(sheet, Level);
            return TalentBonuses.Apply(TalentsFor(kit), sheet);
        }
    }
}
