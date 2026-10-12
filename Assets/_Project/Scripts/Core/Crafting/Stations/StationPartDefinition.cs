using System;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>
    /// Pieza de estación (GDD §17.3): crisol, fuelle, olla de hierro… Su <see cref="Id"/> es el que
    /// citan las <c>RequiredParts</c> de las recetas. Bonus y durabilidad son datos.
    /// </summary>
    public sealed class StationPartDefinition
    {
        public string Id { get; }
        public StationType Station { get; }
        public int MaxDurability { get; }
        public int QualityBonus { get; }
        public float TimeBonus { get; }
        public float FuelSaving { get; }

        public StationPartDefinition(string id, StationType station, int maxDurability = 100,
            int qualityBonus = 0, float timeBonus = 0f, float fuelSaving = 0f)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id de pieza vacío.", nameof(id));
            if (!StationTypes.IsDefined(station)) throw new ArgumentOutOfRangeException(nameof(station));
            Id = id;
            Station = station;
            MaxDurability = Guard.Positive(maxDurability, nameof(maxDurability));
            QualityBonus = Guard.NonNegative(qualityBonus, nameof(qualityBonus));
            TimeBonus = RequireFraction(timeBonus, nameof(timeBonus));
            FuelSaving = RequireFraction(fuelSaving, nameof(fuelSaving));
        }

        public bool FitsIn(StationType station) => Station == station;

        private static float RequireFraction(float value, string name) =>
            value >= 0f && value < 1f ? value : throw new ArgumentOutOfRangeException(name, value, "Debe estar en [0, 1).");

        public override string ToString() => $"{Id} ({Station})";
    }
}
