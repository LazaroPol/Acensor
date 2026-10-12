using System.Collections.Generic;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Common;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;
using G = Specimen.Core.Items.Catalog.GearIds;

namespace Specimen.Core.Sandbox
{
    /// <summary>
    /// Equipo que sueltan las criaturas, aparte de los materiales: los diablillos dejan guantes o dagas, los
    /// puglins yelmos, botas o (raro) su hacha, y el Guardián siempre una pieza épica.
    /// </summary>
    public static class SandboxGearLoot
    {
        public const float ImpChance = 0.12f;
        public const float PuglinChance = 0.15f;

        public static IReadOnlyList<LootDrop> Roll(BestiaryEntry entry, IRandom random)
        {
            Guard.NotNull(entry, nameof(entry));
            Guard.NotNull(random, nameof(random));
            var drops = new List<LootDrop>();
            if (entry.Id == ActOneBoss.Id) drops.Add(Drop(random.Chance(0.5f) ? G.MandobleGuardian : G.CorazaGuardian));
            else if (entry.Model == MonsterModel.Imp && random.Chance(ImpChance))
                drops.Add(Drop(random.Chance(0.5f) ? G.GuantesChamuscados : G.DagaDiablillo));
            else if (entry.Model == MonsterModel.Puglin && random.Chance(PuglinChance))
                drops.Add(Drop(Pick(random, G.YelmoAbollado, G.BotasCuero, G.HachaPuglin)));
            return drops;
        }

        private static string Pick(IRandom random, string a, string b, string rare)
        {
            float roll = random.Value();
            return roll < 0.45f ? a : roll < 0.9f ? b : rare;
        }

        private static LootDrop Drop(string id) => new LootDrop(ItemCatalog.Get(id), 1);
    }
}
