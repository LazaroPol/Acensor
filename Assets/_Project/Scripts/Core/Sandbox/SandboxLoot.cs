using System.Collections.Generic;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;

namespace Specimen.Core.Sandbox
{
    /// <summary>Botín de prueba: tabla de materiales y consumibles; el jefe tira en el anillo 3 y suelta más.</summary>
    public static class SandboxLoot
    {
        public static LootTable Table() => new LootTable(new[]
        {
            new LootEntry(new ItemDefinition("chatarra", "Chatarra", ItemCategory.Material, Rarity.Common, 0, 20), 10f, 1, 1, 3),
            new LootEntry(new ItemDefinition("venda", "Venda", ItemCategory.Consumable, Rarity.Common, 0, 5), 6f),
            new LootEntry(new ItemDefinition("nucleo", "Núcleo inestable", ItemCategory.Material, Rarity.Rare, 0, 5), 2f),
            new LootEntry(new ItemDefinition("reliquia", "Reliquia de ceniza", ItemCategory.Material, Rarity.Epic, 0, 1), 1f, 2)
        }, new RarityRingWeighting());

        public static int RingOf(BestiaryEntry entry)
            => Guard.NotNull(entry, nameof(entry)).Id == ActOneBoss.Id ? ActOneBoss.Ring : DepthRings.Min;

        public static IReadOnlyList<LootDrop> DropsFor(BestiaryEntry entry, IRandom random)
        {
            int ring = RingOf(entry);
            return Table().Roll(ring, ring == DepthRings.Min ? 1 : 3, Guard.NotNull(random, nameof(random)));
        }
    }
}
