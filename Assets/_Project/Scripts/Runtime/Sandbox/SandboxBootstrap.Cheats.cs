using Specimen.Core.Crafting.Content;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Atajos de prueba para la progresión: F3 da experiencia y F4 un lote de materiales básicos
    /// y de oficio (cobre, leña y un crisol para probar las estaciones, OF-15).</summary>
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
            _context.Feed.Add($"F4 · prueba: +{TestMaterials} de cada material básico y de oficio, y un crisol.");
        }
    }
}
