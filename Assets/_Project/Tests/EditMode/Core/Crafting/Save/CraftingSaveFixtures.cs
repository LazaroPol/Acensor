using Specimen.Core.Crafting.Save;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Save
{
    internal static class CraftingSaveFixtures
    {
        public static CraftingSaveData Full()
        {
            var d = new CraftingSaveData();
            d.Skills.Add(new SkillEntry(ProfessionId.Herrero, 7, 340));
            d.Skills.Add(new SkillEntry(ProfessionId.Sastre, 2, 15));
            d.KnownRecipes.Add("herrero.espada_acero");
            d.ReadVolumes.Add("libro_forjador.vol2");
            d.Stations.Add(new StationEntry("fragua_aldea", 2));
            d.Parts.Add(new PartEntry("fragua_aldea", 0, "pieza.crisol", 37));
            d.Slots.Add(new SlotEntry("fragua_aldea", "fuel", 0, "carbon_vegetal", 4));
            d.Jobs.Add(new JobEntry("fragua_aldea", "herrero.espada_acero", 12.5, ProfessionId.Herrero, 7));
            d.Jobs.Add(new JobEntry("fragua_aldea", "herrero.clavos", 0.1, ProfessionId.Herrero, 7));
            d.Items.Add(new ItemEntry("ab12", "espada_hierro", Quality.Fine, 120, new[] { "mod.tiras_cuero", "mod.clavo" }));
            d.Items.Add(new ItemEntry("cd34", "arco_corto", Quality.Crude, 40));
            d.Inventory.Add(InventoryEntry.Instance("bolsa", 3, "ab12"));
            d.Inventory.Add(InventoryEntry.Stack("bolsa", 4, "madera_pino", 12));
            d.Inventory.Add(InventoryEntry.Instance("cofre", 0, "cd34"));
            return d;
        }

        public const string Sample =
            "oficios v1\nskill=Herrero:7:340\nknown=herrero.espada_acero\nitem=ab12:espada_hierro:4:120:mod.a\ninv=bolsa:3:ab12\n";
    }
}
