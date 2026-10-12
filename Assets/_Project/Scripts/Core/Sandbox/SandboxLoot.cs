using System.Collections.Generic;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Common;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;
using I = Specimen.Core.Items.Catalog.ItemIds;

namespace Specimen.Core.Sandbox
{
    /// <summary>
    /// Botín del bestiario con los materiales de las profesiones: los diablillos sueltan tela, chatarra y
    /// cristal; los puglins, cuero y carne. El jefe tira en el anillo 3 (reliquias) y suelta más.
    /// </summary>
    public static class SandboxLoot
    {
        public static LootTable TableFor(MonsterModel model) => model == MonsterModel.Imp
            ? Table((I.Tela, 10f, 1, 2), (I.Chatarra, 8f, 1, 2), (I.Cristal, 3f, 1, 1), (I.Venda, 3f, 1, 1),
                (I.Nucleo, 1.5f, 1, 1), (I.Reliquia, 1f, 1, 1))
            : Table((I.Cuero, 10f, 1, 2), (I.Carne, 10f, 1, 2), (I.Chatarra, 4f, 1, 2), (I.Nucleo, 1.5f, 1, 1),
                (I.Reliquia, 1f, 1, 1));

        public static int RingOf(BestiaryEntry entry)
            => Guard.NotNull(entry, nameof(entry)).Id == ActOneBoss.Id ? ActOneBoss.Ring : DepthRings.Min;

        public static IReadOnlyList<LootDrop> DropsFor(BestiaryEntry entry, IRandom random)
        {
            int ring = RingOf(entry);
            return TableFor(entry.Model).Roll(ring, ring == DepthRings.Min ? 1 : 3, Guard.NotNull(random, nameof(random)));
        }

        private static LootTable Table(params (string id, float weight, int min, int max)[] entries)
        {
            var list = new List<LootEntry>();
            foreach (var (id, weight, min, max) in entries)
            {
                int minRing = id == I.Reliquia ? 2 : DepthRings.Min;
                list.Add(new LootEntry(ItemCatalog.Get(id), weight, minRing, min, max));
            }
            return new LootTable(list, new RarityRingWeighting());
        }
    }
}
