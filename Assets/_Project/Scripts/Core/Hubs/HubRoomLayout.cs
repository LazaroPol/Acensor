using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Prototype;
using Specimen.Core.Visuals;

namespace Specimen.Core.Hubs
{
    /// <summary>
    /// Sala de un hub (Armería o Ascensor) por la que se camina: suelo y muros greybox, una fila de
    /// «estaciones» (pedestales de Loot) a lo largo del muro norte y el Ascensor en el muro este.
    /// </summary>
    public sealed class HubRoomLayout : ILevelLayout
    {
        public HubRoomLayout(int width, int depth, int stationCount)
        {
            Width = Guard.Positive(width, nameof(width));
            Depth = Guard.Positive(depth, nameof(depth));
            StationCount = Guard.NonNegative(stationCount, nameof(stationCount));
        }

        public int Width { get; }
        public int Depth { get; }
        public int StationCount { get; }

        /// <summary>Centro de la sala, donde aparece el jugador.</summary>
        public (float X, float Z) SpawnPoint => (Width / 2f, Depth / 2f);

        /// <summary>Puerta del Ascensor: centro del muro este, dentro de la sala.</summary>
        public (float X, float Z) ElevatorPoint => (Width - 1.5f, Depth / 2f);

        /// <summary>Posición de la estación <paramref name="index"/>, repartidas a lo largo del muro norte.</summary>
        public (float X, float Z) StationPoint(int index)
        {
            if (index < 0 || index >= StationCount) throw new System.ArgumentOutOfRangeException(nameof(index));
            float step = (Width - 2f) / (StationCount + 1f);
            return (1f + step * (index + 1), Depth - 1.5f);
        }

        public IReadOnlyList<LevelPlacement> Build()
        {
            var result = new List<LevelPlacement>();
            for (var x = 0; x < Width; x++)
                for (var z = 0; z < Depth; z++)
                    result.Add(new LevelPlacement(VisualId.Suelo, x, 0f, z));
            for (var x = 0; x < Width; x++)
            {
                result.Add(new LevelPlacement(VisualId.Muro, x, 0f, 0f));
                result.Add(new LevelPlacement(VisualId.Muro, x, 0f, Depth));
            }
            for (var z = 0; z < Depth; z++)
            {
                result.Add(new LevelPlacement(VisualId.Muro, 0f, 0f, z, GreyboxRoomLayout.WestYaw));
                result.Add(new LevelPlacement(VisualId.Muro, Width, 0f, z, GreyboxRoomLayout.WestYaw));
            }
            for (var i = 0; i < StationCount; i++)
            {
                var (x, z) = StationPoint(i);
                result.Add(new LevelPlacement(VisualId.Loot, x, 0f, z));
            }
            var (ex, ez) = ElevatorPoint;
            result.Add(new LevelPlacement(VisualId.Ascensor, ex, 0f, ez));
            return result;
        }
    }
}
