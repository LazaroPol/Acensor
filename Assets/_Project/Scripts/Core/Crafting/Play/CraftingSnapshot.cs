using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Foto del progreso de oficios del Campo de pruebas (GDD §17.7) para <see cref="CraftingSave"/>:
    /// oficios, recetas y lecturas, estaciones (nivel, piezas, ranuras y cola) y la bolsa con sus instancias.
    /// </summary>
    public static class CraftingSnapshot
    {
        public const string Bag = "bolsa";

        public static CraftingSaveData Capture(CraftingProfile profile, IEnumerable<StationSession> stations,
            GearModBench mods = null)
        {
            Guard.NotNull(profile, nameof(profile));
            var data = new CraftingSaveData();
            foreach (var s in profile.Skills.OrderBy(s => s.Profession))
                data.Skills.Add(new SkillEntry(s.Profession, s.Level, s.Xp));
            data.KnownRecipes.AddRange(profile.Knowledge.LearnedRecipes);
            data.ReadVolumes.AddRange(profile.Knowledge.ReadKnowledge);
            var items = new InstanceWriter(data, mods);
            items.Write(Bag, profile.Bag);
            foreach (var session in (stations ?? Enumerable.Empty<StationSession>()).OrderBy(s => s.Station.Type))
                CaptureStation(data, session, items);
            return data;
        }

        public static string InputOf(string station) => station + ".in";
        public static string OutputOf(string station) => station + ".out";
        public static string FuelOf(string station) => station + ".fuel";

        private static void CaptureStation(CraftingSaveData data, StationSession session, InstanceWriter items)
        {
            var station = session.Station;
            string name = station.Type.ToString();
            data.Stations.Add(new StationEntry(name, station.Level));
            for (int i = 0; i < station.Parts.Parts.Count; i++)
            {
                var part = station.Parts.Parts[i];
                data.Parts.Add(new PartEntry(name, i, part.Id, part.Durability));
            }
            items.Write(InputOf(name), station.Input);
            items.Write(OutputOf(name), station.Output);
            if (station.HasFuelSlots) items.Write(FuelOf(name), station.Fuel);
            foreach (var job in session.Queue.Jobs)
                data.Jobs.Add(new JobEntry(name, job.Recipe.Id, job.Remaining, job.Crafter.Profession, job.Crafter.Level));
        }

        /// <summary>Escribe ranuras de inventario: instancias únicas (calidad, desgaste, mods) o pilas.</summary>
        private sealed class InstanceWriter
        {
            private readonly CraftingSaveData _data;
            private readonly GearModBench _mods;
            private int _next;

            public InstanceWriter(CraftingSaveData data, GearModBench mods)
            {
                _data = data;
                _mods = mods;
            }

            public void Write(string inventory, IInventory source)
            {
                for (int slot = 0; slot < source.Stacks.Count; slot++)
                {
                    var stack = source.Stacks[slot];
                    var mods = _mods?.ModsOf(stack.Item).Select(m => m.Id).ToList() ?? new List<string>();
                    if (!IsUnique(stack, mods))
                    {
                        _data.Inventory.Add(InventoryEntry.Stack(inventory, slot, stack.Id, stack.Quantity));
                        continue;
                    }
                    string id = "i" + (++_next);
                    _data.Items.Add(new ItemEntry(id, stack.Id, stack.Item.Quality, stack.Item.Durability.Current, mods));
                    _data.Inventory.Add(InventoryEntry.Instance(inventory, slot, id));
                }
            }

            private static bool IsUnique(ItemStack stack, IReadOnlyCollection<string> mods) =>
                stack.Quantity == 1 && (stack.Item.Quality != Quality.None || mods.Count > 0
                    || stack.Item.Durability.Current < stack.Item.Durability.Max);
        }
    }
}
