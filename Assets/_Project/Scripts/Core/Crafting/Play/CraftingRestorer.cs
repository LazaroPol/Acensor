using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Aplica un guardado de oficios al Campo de pruebas (GDD §17.7). Lo que el catálogo ya no conoce
    /// (ids retirados) se ignora en lugar de romper la carga. Un inventario sin entradas guardadas no se toca
    /// (la primera vez se conserva lo que el jugador ya llevaba).
    /// </summary>
    public sealed class CraftingRestorer
    {
        private readonly ICraftingCatalog _catalog;
        private readonly ItemResolver _items;
        private readonly GearModBench _mods;

        public CraftingRestorer(ICraftingCatalog catalog, GearModBench mods = null)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            _items = new ItemResolver(catalog);
            _mods = mods;
        }

        public void Apply(CraftingSaveData data, CraftingProfile profile, StationSessionFactory stations)
        {
            Guard.NotNull(data, nameof(data));
            Guard.NotNull(profile, nameof(profile));
            Guard.NotNull(stations, nameof(stations));
            foreach (var s in data.Skills) profile.Skill(s.Profession).Restore(s.Level, s.Xp);
            foreach (var id in data.KnownRecipes) profile.Knowledge.RestoreRecipe(id);
            foreach (var id in data.ReadVolumes)
            {
                var k = _catalog.Knowledge.FirstOrDefault(x => x.Id == id);
                if (k != null) profile.Knowledge.RestoreRead(k);
            }
            var instances = data.Items.ToDictionary(i => i.InstanceId);
            Fill(profile.Bag, CraftingSnapshot.Bag, data, instances);
            foreach (var entry in data.Stations)
            {
                if (!Enum.TryParse(entry.Station, out StationType type)) continue;
                RestoreStation(stations.Create(type), entry, data, instances);
            }
        }

        private void RestoreStation(StationSession session, StationEntry entry, CraftingSaveData data,
            IReadOnlyDictionary<string, ItemEntry> instances)
        {
            string name = entry.Station;
            session.RestoreLevel(entry.Level);
            foreach (var part in data.Parts.Where(p => p.Station == name).OrderBy(p => p.Slot))
                session.RestorePart(part.ItemId, part.Durability);
            Fill(session.Station.Input, CraftingSnapshot.InputOf(name), data, instances);
            Fill(session.Station.Output, CraftingSnapshot.OutputOf(name), data, instances);
            if (session.Station.HasFuelSlots) Fill(session.Station.Fuel, CraftingSnapshot.FuelOf(name), data, instances);
            foreach (var job in data.Jobs.Where(j => j.Station == name))
                if (_catalog.Recipes.TryGet(job.RecipeId, out var recipe))
                    session.RestoreJob(recipe, job.Profession, (float)job.RemainingSeconds);
        }

        private void Fill(IInventory target, string inventory, CraftingSaveData data,
            IReadOnlyDictionary<string, ItemEntry> instances)
        {
            var slots = data.Inventory.Where(e => e.Inventory == inventory).OrderBy(e => e.Slot).ToList();
            if (slots.Count == 0) return;
            foreach (var stack in target.Stacks.ToList()) target.RemoveInstance(stack.Item);
            foreach (var slot in slots)
            {
                if (!slot.IsInstance)
                {
                    var def = _items.Find(slot.Ref);
                    if (def != null) target.Add(ItemInstance.CreateNew(def), slot.Count);
                    continue;
                }
                if (instances.TryGetValue(slot.Ref, out var saved) && Build(saved) is ItemInstance item) target.Add(item);
            }
        }

        private ItemInstance Build(ItemEntry saved)
        {
            var def = _items.Find(saved.ItemId);
            if (def == null) return null;
            var item = saved.Quality == Quality.None
                ? ItemInstance.CreateNew(def)
                : ItemInstance.CreateNew(def, _catalog.Quality.Grade(saved.Quality));
            int wear = item.Durability.Current - saved.Durability;
            if (wear > 0 && !item.Durability.IsIndestructible) item.Durability.Wear(wear);
            _mods?.Restore(item, saved.Mods);
            return item;
        }
    }
}
