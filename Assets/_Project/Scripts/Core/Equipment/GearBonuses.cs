using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Equipment
{
    /// <summary>Vuelca el equipo en la hoja de stats: armadura total y bonos planos de cada pieza (idempotente).</summary>
    public static class GearBonuses
    {
        private static readonly object Source = new object();

        public static void Apply(CharacterEquipment equipment, StatSheet sheet)
        {
            Guard.NotNull(equipment, nameof(equipment));
            Guard.NotNull(sheet, nameof(sheet));
            foreach (var type in sheet.Types.ToList()) sheet.Get(type).RemoveAllFromSource(Source);
            float armor = equipment.TotalArmor;
            if (armor > 0f && sheet.Has(StatType.Armor)) sheet.AddModifier(StatType.Armor, StatModifier.Flat(armor, Source));
            foreach (var pair in equipment.Items)
            {
                if (!GearCatalog.TryGet(pair.Value.Id, out var piece)) continue;
                foreach (var bonus in piece.Bonuses)
                    if (sheet.Has(bonus.Key)) sheet.AddModifier(bonus.Key, StatModifier.Flat(bonus.Value, Source));
            }
        }
    }
}
