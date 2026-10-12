using System.Collections.Generic;

namespace Specimen.Core.World
{
    /// <summary>
    /// Mapa del poblado medieval que rodea la Armería (sala de 22 × 18 m en el origen, puerta al sur):
    /// plaza con casas, caminos, bosque de ceniza al oeste, cantera al sur y campamento de puglins al este.
    /// Coordenadas en metros; el sur es -Z.
    /// </summary>
    public sealed partial class VillageLayout
    {
        public const float MinX = -42f, MaxX = 58f, MinZ = -58f, MaxZ = 22f;
        public const float GateX = 11.5f;
        public const float PlazaX = 11.5f, PlazaZ = -14f;

        public IReadOnlyList<MapZone> Zones { get; } = new[]
        {
            new MapZone("Armería", "Bancos de profesión y cambio de kit", 0f, 0f, 22f, 18f),
            new MapZone("Plaza del Poblado", "Corazón del pueblo", 3f, -20f, 20f, -8f),
            new MapZone("Bosque de Ceniza", "Madera y hierba de ceniza", -42f, -36f, -8f, 4f),
            new MapZone("Cantera", "Piedra, hierro y diablillos", -1f, -58f, 24f, -36f),
            new MapZone("Campamento Puglin", "Puglins y el Guardián", 30f, -28f, 58f, 0f)
        };

        public IReadOnlyList<HouseSpot> Houses { get; } = new[]
        {
            new HouseSpot("casa-herrera", 3f, -8f, 90f, 4, 6, HouseStyle.Yeso),
            new HouseSpot("casa-posada", 20f, -8f, -90f, 6, 6, HouseStyle.Ladrillo),
            new HouseSpot("casa-sastre", 2f, -21f, 90f, 4, 4, HouseStyle.Yeso),
            new HouseSpot("casa-alcaldia", 21f, -22f, -90f, 6, 8, HouseStyle.Ladrillo),
            new HouseSpot("casa-molinero", 4f, -30f, 90f, 4, 6, HouseStyle.Ladrillo),
            new HouseSpot("casa-curandera", 20f, -31f, -90f, 6, 6, HouseStyle.Yeso)
        };

        public IReadOnlyList<WorldSpot> Props { get; } = new[]
        {
            new WorldSpot("Prop_Wagon", 14.5f, -21f, 30f),
            new WorldSpot("Prop_Crate", 8.3f, -9.2f), new WorldSpot("Prop_Crate", 9.3f, -9.8f, 20f),
            new WorldSpot("Prop_WoodenFence_Single", 26f, -11.5f), new WorldSpot("Prop_WoodenFence_Single", 28.5f, -11.5f),
            new WorldSpot("Prop_WoodenFence_Single", 26f, -16.5f), new WorldSpot("Prop_WoodenFence_Single", 28.5f, -16.5f),
            new WorldSpot("Prop_Support", 30.5f, -11.5f), new WorldSpot("Prop_Support", 30.5f, -16.5f),
            new WorldSpot("Prop_MetalFence_Ornament", 9f, -2.5f), new WorldSpot("Prop_MetalFence_Ornament", 14f, -2.5f)
        };

        public IReadOnlyList<WorldSpot> Camps { get; } = new[]
        {
            new WorldSpot("diablillo", 5f, -44f, 0f), new WorldSpot("diablillo-toxico", 18f, -42f, 0f),
            new WorldSpot("diablillo-voltaico", 12f, -51f, 0f), new WorldSpot("puglin", 37f, -8f, 270f),
            new WorldSpot("puglin-bruto", 41f, -19f, 270f), new WorldSpot("guardian-acto-1", 51f, -14f, 270f)
        };

        public MapZone ZoneAt(float x, float z)
        {
            foreach (var zone in Zones) if (zone.Contains(x, z)) return zone;
            return null;
        }
    }
}
