using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Botín de oficios del poblado y de los pisos (GDD §17.7): conocimientos, piezas de estación y mods
    /// en una tabla por anillo. Los conocimientos de nivel alto solo salen en anillos profundos.
    /// </summary>
    public sealed class CraftingLoot
    {
        public const float DefaultChance = 0.3f;
        public const float ModWeight = 3f;
        public const float PartWeight = 2f;

        public CraftingLoot(ICraftingCatalog catalog)
        {
            Guard.NotNull(catalog, nameof(catalog));
            var entries = new List<LootEntry>();
            foreach (var k in catalog.Knowledge)
                entries.Add(new LootEntry(KnowledgeItems.For(k), WeightOf(k.Type), RingOf(k.MinLevel)));
            foreach (var p in catalog.Parts)
                entries.Add(new LootEntry(p.Item, PartWeight));
            foreach (var m in catalog.Mods)
                if (m.Item != null) entries.Add(new LootEntry(m.Item, ModWeight, DepthRings.Min, 1, 2));
            Table = new LootTable(entries, new RarityRingWeighting());
        }

        public LootTable Table { get; }

        /// <summary>Con probabilidad <paramref name="chance"/> suelta una pieza de oficio del anillo.</summary>
        public IReadOnlyList<LootDrop> Roll(int ring, IRandom random, float chance = DefaultChance)
        {
            Guard.NotNull(random, nameof(random));
            var drops = new List<LootDrop>();
            if (Table.HasLootIn(ring) && random.Chance(chance)) drops.Add(Table.Roll(ring, random));
            return drops;
        }

        public static int RingOf(int minLevel) => minLevel <= 5 ? DepthRings.Min : minLevel <= 10 ? 2 : DepthRings.Max;

        public static float WeightOf(KnowledgeType type) => type switch
        {
            KnowledgeType.Scroll => 6f,
            KnowledgeType.Blueprint => 4f,
            KnowledgeType.Treatise => 2f,
            _ => 0.5f
        };
    }
}
