using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Planifica qué pilas gastar para unos ingredientes por objeto o por etiqueta sin tocar el
    /// inventario. Resuelve primero los ingredientes concretos y luego las etiquetas con lo que
    /// queda; por etiqueta gasta antes el menor tier válido y, a igual tier, lo que tenga menos
    /// unidades (salvo que el jugador haya fijado un ítem con <see cref="IngredientChoice"/>).
    /// </summary>
    public static class TagConsumptionPlanner
    {
        public static ConsumptionPlan Plan(IReadOnlyList<IngredientSpec> specs, IInventory inventory,
            IReadOnlyList<IngredientChoice> choices = null)
        {
            Guard.NotNull(specs, nameof(specs));
            Guard.NotNull(inventory, nameof(inventory));
            foreach (var spec in specs)
                if (spec == null) throw new ArgumentException("Ingrediente nulo.", nameof(specs));
            var pinned = PinnedBySpec(specs, choices);
            var builder = new PlanBuilder(inventory);
            foreach (var spec in specs)
                if (!spec.IsByTag && builder.Take(spec.Item, spec.Quantity) < spec.Quantity)
                    return ConsumptionPlan.Missing;
            for (int i = 0; i < specs.Count; i++)
                if (specs[i].IsByTag && !PlanTag(specs[i], pinned[i], builder))
                    return ConsumptionPlan.Missing;
            return builder.Build();
        }

        private static bool PlanTag(IngredientSpec spec, string pinnedId, PlanBuilder builder)
        {
            int remaining = spec.Quantity;
            var pinnedDef = pinnedId == null ? null : builder.Definition(pinnedId);
            if (pinnedDef != null)
            {
                if (!spec.Accepts(pinnedDef))
                    throw new ArgumentException($"{pinnedId} no sirve para el ingrediente {spec}.");
                remaining -= builder.Take(pinnedDef, remaining);
            }
            foreach (var def in builder.CandidatesFor(spec))
            {
                if (remaining == 0) break;
                remaining -= builder.Take(def, remaining);
            }
            return remaining == 0;
        }

        private static string[] PinnedBySpec(IReadOnlyList<IngredientSpec> specs,
            IReadOnlyList<IngredientChoice> choices)
        {
            var pinned = new string[specs.Count];
            if (choices == null) return pinned;
            foreach (var c in choices)
            {
                if (c == null) continue;
                if (c.SpecIndex >= specs.Count || !specs[c.SpecIndex].IsByTag)
                    throw new ArgumentException($"La elección {c.SpecIndex} no apunta a un ingrediente por etiqueta.");
                pinned[c.SpecIndex] = c.ItemId;
            }
            return pinned;
        }
    }
}
