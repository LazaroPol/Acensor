using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>
    /// Estación de trabajo (GDD §17.3): tipo + nivel (1-3) + ranuras de entrada, salida, combustible
    /// y piezas. La cola de fabricación llega en OF-09.
    /// </summary>
    public sealed partial class Workstation
    {
        public const int InputSlots = 6;
        public const int OutputSlots = 6;
        public const int FuelSlots = 2;
        public const int BaseQueueSize = 2;

        public StationType Type { get; }
        public int Level { get; private set; } = RecipeDefinition.MinStationLevelValue;
        public int QueueSize { get; private set; } = BaseQueueSize;
        public int LevelQualityBonus { get; private set; }

        public Inventory Input { get; } = new Inventory(InputSlots);
        public Inventory Output { get; } = new Inventory(OutputSlots);

        /// <summary>Ranuras de combustible; null si la estación no quema nada.</summary>
        public Inventory Fuel { get; }

        public PartSlots Parts { get; }

        public Workstation(StationType type)
        {
            Type = type;
            Parts = new PartSlots(type);
            Fuel = UsesFuel(type) ? new Inventory(FuelSlots) : null;
        }

        public bool HasFuelSlots => Fuel != null;
        public bool IsMaxLevel => Level >= RecipeDefinition.MaxStationLevelValue;

        /// <summary>Calidad extra total: nivel de la estación + piezas sanas.</summary>
        public int QualityBonus => LevelQualityBonus + Parts.QualityBonus;

        public PartPlaceResult PlacePart(StationPart part) => Parts.Place(part);

        public StationPart RemovePart(string partId) => Parts.Remove(partId);

        /// <summary>Solo Fragua, Hogar de cocina y Mesa de alquimia usan combustible.</summary>
        public static bool UsesFuel(StationType type) =>
            type == StationType.Fragua || type == StationType.HogarCocina || type == StationType.MesaAlquimia;

        public override string ToString() => $"{Type} nv{Level}";
    }
}
