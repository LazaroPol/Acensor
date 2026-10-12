using System.Linq;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Play;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Atajos de prueba para la progresión: F3 da experiencia y F4 un lote de materiales básicos
    /// y de oficio (cobre, leña y un crisol para probar las estaciones, OF-15) y lo de OF-16.</summary>
    public sealed partial class SandboxBootstrap
    {
        private const int TestXp = 120;
        private const int TestMaterials = 5;

        private static readonly string[] Basics =
        {
            ItemIds.Madera, ItemIds.Piedra, ItemIds.MineralHierro, ItemIds.Hierba, ItemIds.Tela,
            ItemIds.Cuero, ItemIds.Carne, ItemIds.Chatarra, ItemIds.Cristal
        };

        private static readonly string[] TradeBasics = { "mineral_cobre", "lingote_cobre", FuelIds.Lena };

        private void TestShortcuts(SandboxInput input)
        {
            if (input.GiveXp.WasPressedThisFrame()) _context.GrantXp(TestXp, "F3 · prueba", Core.Progression.XpSource.Combate);
            if (!input.GiveMaterials.WasPressedThisFrame()) return;
            foreach (string id in Basics)
                _context.Inventory.Add(ItemInstance.CreateNew(ItemCatalog.Get(id)), TestMaterials);
            foreach (string id in TradeBasics)
                _context.Inventory.Add(ItemInstance.CreateNew(ContentItems.Get(id)), TestMaterials);
            _context.Inventory.Add(ItemInstance.CreateNew(ContentItems.Get("pieza.crisol")));
            GiveTradeTests();
            _context.Feed.Add($"F4 · prueba: +{TestMaterials} de cada material básico y de oficio, un crisol, " +
                              "un pergamino y un plano de Herrero, una piedra atada y un palo de madera bueno.");
        }

        /// <summary>OF-16: lo justo para probar lectura y mods; sube Herrero al nivel de los planos.</summary>
        private void GiveTradeTests()
        {
            var data = _context.CraftingData;
            var smith = _context.Crafting.Skill(ProfessionId.Herrero);
            smith.Restore(Mathf.Max(smith.Level, DefaultKnowledge.BlueprintLevel), smith.Xp);
            foreach (var type in new[] { KnowledgeType.Scroll, KnowledgeType.Blueprint })
            {
                var k = data.Knowledge.First(x => x.Type == type && x.Profession == ProfessionId.Herrero);
                _context.Inventory.Add(ItemInstance.CreateNew(KnowledgeItems.For(k)));
            }
            _context.Inventory.Add(ItemInstance.CreateNew(_context.Mods.Mod(DefaultMods.PiedraAtada).Item));
            if (data.TryGetItem("palo_madera", out var palo) || ItemCatalog.TryGet("palo_madera", out palo))
                _context.Inventory.Add(ItemInstance.CreateNew(palo, data.Quality.Grade(Quality.Good)));
        }
    }
}
