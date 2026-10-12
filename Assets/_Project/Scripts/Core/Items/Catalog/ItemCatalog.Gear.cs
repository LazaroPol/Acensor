using System.Collections.Generic;
using G = Specimen.Core.Items.Catalog.GearIds;

namespace Specimen.Core.Items.Catalog
{
    /// <summary>Equipo inicial de los kits y equipo que sueltan las criaturas (sus stats están en GearCatalog).</summary>
    public static partial class ItemCatalog
    {
        private static void AddEquipment(List<ItemDefinition> list)
        {
            Gear(list, G.MazaRecluta, "Maza de recluta", ItemCategory.Weapon, Rarity.Common, 50);
            Gear(list, G.LanzaVigia, "Lanza de vigía", ItemCategory.Weapon, Rarity.Common, 50);
            Gear(list, G.BastonAprendiz, "Bastón de aprendiz", ItemCategory.Weapon, Rarity.Common, 50);
            Gear(list, G.CayadoCeniza, "Cayado de ceniza", ItemCategory.Weapon, Rarity.Common, 50);
            Gear(list, G.DagaGastada, "Daga gastada", ItemCategory.Weapon, Rarity.Common, 50);
            Gear(list, G.VaritaChispas, "Varita de chispas", ItemCategory.Weapon, Rarity.Common, 50);
            Gear(list, G.CotaAnillas, "Cota de anillas gastada", ItemCategory.Armor, Rarity.Common, 60);
            Gear(list, G.JubonCuero, "Jubón de cuero", ItemCategory.Armor, Rarity.Common, 60);
            Gear(list, G.TunicaLino, "Túnica de lino", ItemCategory.Armor, Rarity.Common, 60);
            Gear(list, G.PantalonesTela, "Pantalones de tela", ItemCategory.Armor, Rarity.Common, 40);
            Gear(list, G.GuantesChamuscados, "Guantes chamuscados", ItemCategory.Armor, Rarity.Uncommon, 40);
            Gear(list, G.DagaDiablillo, "Daga de diablillo", ItemCategory.Weapon, Rarity.Uncommon, 60);
            Gear(list, G.YelmoAbollado, "Yelmo abollado", ItemCategory.Armor, Rarity.Uncommon, 60);
            Gear(list, G.BotasCuero, "Botas de cuero curtido", ItemCategory.Armor, Rarity.Uncommon, 50);
            Gear(list, G.HachaPuglin, "Hacha de puglin", ItemCategory.Weapon, Rarity.Rare, 70);
            Gear(list, G.MandobleGuardian, "Mandoble del Guardián", ItemCategory.Weapon, Rarity.Epic, 100);
            Gear(list, G.CorazaGuardian, "Coraza del Guardián", ItemCategory.Armor, Rarity.Epic, 100);
        }
    }
}
