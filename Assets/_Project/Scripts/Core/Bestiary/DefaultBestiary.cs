using System.Collections.Generic;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Core.Bestiary
{
    /// <summary>Criaturas disponibles con los modelos actuales. Stats en piso 1 / anillo 1 (provisionales).</summary>
    public static class DefaultBestiary
    {
        public static IReadOnlyList<BestiaryEntry> All() => new[]
        {
            Entry("diablillo", "Diablillo de brasa", 40f, 6f, 2f, DamageType.Fire, MonsterModel.Imp, 1, 1f),
            Entry("diablillo-toxico", "Diablillo pútrido", 35f, 5f, 2f, DamageType.Toxic, MonsterModel.Imp, 2, 0.9f),
            Entry("diablillo-voltaico", "Diablillo voltaico", 35f, 7f, 1f, DamageType.Shock, MonsterModel.Imp, 3, 0.95f),
            Entry("puglin", "Puglin", 60f, 8f, 6f, DamageType.Physical, MonsterModel.Puglin, 1, 1f),
            Entry("puglin-bruto", "Puglin bruto", 110f, 12f, 10f, DamageType.Physical, MonsterModel.Puglin, 2, 1.3f),
            new BestiaryEntry(ActOneBoss.Archetype(), "Guardián del Acto I", MonsterModel.Puglin, 3, 1.8f)
        };

        public static BestiaryEntry Find(string id)
        {
            foreach (var entry in All())
                if (entry.Id == id) return entry;
            return null;
        }

        private static BestiaryEntry Entry(string id, string name, float hp, float dmg, float armor, DamageType type,
            MonsterModel model, int variant, float scale)
        {
            var resistances = Resistances.None;
            if (type != DamageType.Physical) resistances.Set(type, 0.5f);
            var archetype = new EnemyArchetype(id, new EnemyStats(hp, dmg, armor), type, resistances);
            return new BestiaryEntry(archetype, name, model, variant, scale);
        }
    }
}
