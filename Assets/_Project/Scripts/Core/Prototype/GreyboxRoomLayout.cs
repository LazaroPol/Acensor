using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Visuals;

namespace Specimen.Core.Prototype
{
    /// <summary>
    /// Sala simple en cuadrícula de 1 m (GDD 16, PG-02): suelo en cada celda, muros en el
    /// perímetro, una puerta en el centro del muro sur y el jugador en el centro de la sala.
    /// </summary>
    public sealed class GreyboxRoomLayout : ILevelLayout
    {
        public const float WestYaw = 270f;

        public int Width { get; }
        public int Depth { get; }
        public int DoorCell => Width / 2;

        public GreyboxRoomLayout(int width, int depth)
        {
            Width = Guard.Positive(width, nameof(width));
            Depth = Guard.Positive(depth, nameof(depth));
        }

        public IReadOnlyList<LevelPlacement> Build()
        {
            var result = new List<LevelPlacement>();
            AddFloor(result);
            AddWalls(result);
            result.Add(new LevelPlacement(VisualId.Jugador, Width / 2f, 0f, Depth / 2f));
            return result;
        }

        private void AddFloor(List<LevelPlacement> result)
        {
            for (var x = 0; x < Width; x++)
                for (var z = 0; z < Depth; z++)
                    result.Add(new LevelPlacement(VisualId.Suelo, x, 0f, z));
        }

        private void AddWalls(List<LevelPlacement> result)
        {
            for (var x = 0; x < Width; x++)
            {
                var south = x == DoorCell ? VisualId.Puerta : VisualId.Muro;
                result.Add(new LevelPlacement(south, x, 0f, 0f));
                result.Add(new LevelPlacement(VisualId.Muro, x, 0f, Depth));
            }
            for (var z = 0; z < Depth; z++)
            {
                result.Add(new LevelPlacement(VisualId.Muro, 0f, 0f, z, WestYaw));
                result.Add(new LevelPlacement(VisualId.Muro, Width, 0f, z, WestYaw));
            }
        }
    }
}
