using System;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Repair
{
    /// <summary>
    /// Cómo se repara una categoría de equipo: qué material consume y cuánta durabilidad
    /// devuelve cada unidad. Solo se admiten materiales sin desgaste.
    /// </summary>
    public sealed class RepairRule
    {
        public ItemCategory Category { get; }
        public ItemDefinition Material { get; }
        public int PointsPerUnit { get; }

        public RepairRule(ItemCategory category, ItemDefinition material, int pointsPerUnit)
        {
            Category = category;
            Material = Guard.NotNull(material, nameof(material));
            PointsPerUnit = Guard.Positive(pointsPerUnit, nameof(pointsPerUnit));
            if (material.IsDegradable)
                throw new ArgumentException("El material no puede tener durabilidad.", nameof(material));
        }

        /// <summary>Unidades necesarias para recuperar <paramref name="missing"/> puntos.</summary>
        public int UnitsFor(int missing) => (missing + PointsPerUnit - 1) / PointsPerUnit;
    }
}
