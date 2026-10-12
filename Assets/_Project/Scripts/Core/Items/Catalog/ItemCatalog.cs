using System;
using System.Collections.Generic;
using I = Specimen.Core.Items.Catalog.ItemIds;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Catálogo de objetos del juego: materiales (recolección y botín), consumibles y equipo fabricado.</summary>
    public static class ItemCatalog
    {
        private static readonly Dictionary<string, ItemDefinition> Items = Build();

        public static IReadOnlyCollection<ItemDefinition> All => Items.Values;

        public static ItemDefinition Get(string id)
            => Items.TryGetValue(id ?? string.Empty, out var item)
                ? item
                : throw new ArgumentException($"Objeto desconocido: {id}", nameof(id));

        public static bool TryGet(string id, out ItemDefinition item) => Items.TryGetValue(id ?? string.Empty, out item);

        private static Dictionary<string, ItemDefinition> Build()
        {
            var list = new List<ItemDefinition>();
            Material(list, I.Madera, "Madera", Rarity.Common);
            Material(list, I.Piedra, "Piedra", Rarity.Common);
            Material(list, I.MineralHierro, "Mineral de hierro", Rarity.Common);
            Material(list, I.Hierba, "Hierba de ceniza", Rarity.Common);
            Material(list, I.Cristal, "Cristal resonante", Rarity.Uncommon);
            Material(list, I.Tela, "Tela raída", Rarity.Common);
            Material(list, I.Cuero, "Cuero", Rarity.Common);
            Material(list, I.Carne, "Carne", Rarity.Common);
            Material(list, I.Chatarra, "Chatarra", Rarity.Common);
            Material(list, I.Nucleo, "Núcleo inestable", Rarity.Rare);
            Material(list, I.Reliquia, "Reliquia de ceniza", Rarity.Epic);
            Material(list, I.Lingote, "Lingote de hierro", Rarity.Uncommon);
            Consumable(list, I.Venda, "Venda", Rarity.Common);
            Consumable(list, I.PocionVida, "Poción de vida", Rarity.Uncommon);
            Consumable(list, I.PocionMana, "Poción de maná", Rarity.Uncommon);
            Consumable(list, I.Estofado, "Estofado", Rarity.Common);
            Consumable(list, I.Brocheta, "Brocheta", Rarity.Common);
            Consumable(list, I.BombaCeniza, "Bomba de ceniza", Rarity.Rare);
            Gear(list, I.EspadaHierro, "Espada de hierro", ItemCategory.Weapon, Rarity.Uncommon, 60);
            Gear(list, I.PlacaReforzada, "Placa reforzada", ItemCategory.Armor, Rarity.Uncommon, 80);
            Gear(list, I.CapaAcolchada, "Capa acolchada", ItemCategory.Armor, Rarity.Common, 50);
            Gear(list, I.Mochila, "Mochila de cuero", ItemCategory.Tool, Rarity.Uncommon, 0);
            Gear(list, I.Trampa, "Trampa de resorte", ItemCategory.Tool, Rarity.Common, 0, 5);
            Gear(list, I.TorretaPortatil, "Torreta portátil", ItemCategory.Tool, Rarity.Rare, 30);
            Gear(list, I.Ganzua, "Ganzúa", ItemCategory.Tool, Rarity.Common, 0, 10);
            Gear(list, I.KitReparacion, "Kit de reparación", ItemCategory.Tool, Rarity.Common, 0, 5);
            Gear(list, I.TotemPiedra, "Tótem de piedra", ItemCategory.Tool, Rarity.Uncommon, 0);
            Gear(list, I.Idolo, "Ídolo de ceniza", ItemCategory.Tool, Rarity.Epic, 0);
            var result = new Dictionary<string, ItemDefinition>();
            foreach (var item in list) result.Add(item.Id, item);
            return result;
        }

        private static void Material(List<ItemDefinition> list, string id, string name, Rarity rarity)
            => list.Add(new ItemDefinition(id, name, ItemCategory.Material, rarity, 0, 20));

        private static void Consumable(List<ItemDefinition> list, string id, string name, Rarity rarity)
            => list.Add(new ItemDefinition(id, name, ItemCategory.Consumable, rarity, 0, 10));

        private static void Gear(List<ItemDefinition> list, string id, string name, ItemCategory category,
            Rarity rarity, int durability, int stack = 1)
            => list.Add(new ItemDefinition(id, name, category, rarity, durability, durability > 0 ? 1 : stack));
    }
}
