using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;
using I = Specimen.Core.Items.Catalog.ItemIds;

namespace Specimen.Core.Gathering
{
    /// <summary>Qué da cada punto de recolección, cuántos usos tiene y cuánto tarda en volver a crecer.</summary>
    public static class GatheringRules
    {
        public const int UsesPerNode = 3;
        public const float RegrowSeconds = 30f;

        public static string NameOf(GatherKind kind)
        {
            switch (kind)
            {
                case GatherKind.Arbol: return "Árbol";
                case GatherKind.Roca: return "Roca";
                case GatherKind.VetaHierro: return "Veta de hierro";
                default: return "Hierba de ceniza";
            }
        }

        public static IReadOnlyList<LootDrop> Gather(GatherKind kind, IRandom random)
        {
            Guard.NotNull(random, nameof(random));
            var drops = new List<LootDrop>();
            switch (kind)
            {
                case GatherKind.Arbol:
                    drops.Add(Drop(I.Madera, random.Range(1, 3)));
                    break;
                case GatherKind.Roca:
                    drops.Add(Drop(I.Piedra, random.Range(1, 3)));
                    break;
                case GatherKind.VetaHierro:
                    drops.Add(Drop(I.MineralHierro, random.Range(1, 3)));
                    if (random.Chance(0.15f)) drops.Add(Drop(I.Cristal, 1));
                    break;
                default:
                    drops.Add(Drop(I.Hierba, random.Range(1, 4)));
                    if (random.Chance(0.1f)) drops.Add(Drop(I.Cristal, 1));
                    break;
            }
            return drops;
        }

        private static LootDrop Drop(string id, int quantity) => new LootDrop(ItemCatalog.Get(id), quantity);
    }
}
