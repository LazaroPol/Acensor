using System.Collections.Generic;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Resultado del planificador: qué se gastaría, sin haber tocado el inventario. Si faltan
    /// materiales el plan es incompleto y no se puede aplicar.
    /// </summary>
    public sealed class ConsumptionPlan
    {
        public static readonly ConsumptionPlan Missing = new ConsumptionPlan(new PlannedConsumption[0], false);

        public IReadOnlyList<PlannedConsumption> Entries { get; }
        public bool IsComplete { get; }

        internal ConsumptionPlan(IReadOnlyList<PlannedConsumption> entries, bool isComplete)
        {
            Entries = entries;
            IsComplete = isComplete;
        }

        /// <summary>Tier medio ponderado por unidades de lo que realmente se gasta (0 si nada).</summary>
        public float AverageTier
        {
            get
            {
                int units = 0, weighted = 0;
                foreach (var e in Entries)
                {
                    units += e.Quantity;
                    weighted += e.Quantity * e.Item.Tier;
                }
                return units == 0 ? 0f : (float)weighted / units;
            }
        }

        /// <summary>Aplica el plan de forma atómica: gasta lo planificado y añade el resultado, o nada.</summary>
        public CraftResult Apply(ItemDefinition output, int quantity, IInventory inventory)
        {
            if (!IsComplete) return CraftResult.MissingIngredients;
            return CraftTransaction.Apply(Entries, output, quantity, inventory);
        }
    }
}
