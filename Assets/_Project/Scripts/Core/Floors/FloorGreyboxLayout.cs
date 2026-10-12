using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Prototype;
using Specimen.Core.Visuals;

namespace Specimen.Core.Floors
{
    /// <summary>
    /// Convierte un <see cref="FloorLayout"/> en visuales greybox: cada anillo es una fila de salas (suelo
    /// cuadrado), los pasillos unen centros en L y el Ascensor marca la entrada y la Puerta el Hueco de Emergencia.
    /// </summary>
    public sealed class FloorGreyboxLayout : ILevelLayout
    {
        public const int RoomSize = 8;
        public const int Pitch = 20;
        private readonly FloorLayout _floor;

        public FloorGreyboxLayout(FloorLayout floor) => _floor = Guard.NotNull(floor, nameof(floor));

        /// <summary>Centro (x, z) de una sala, en metros.</summary>
        public (float X, float Z) CenterOf(int roomId)
        {
            var room = _floor.Rooms[roomId];
            var index = 0;
            foreach (var other in _floor.RoomsInRing(room.Ring))
            {
                if (other.Id == roomId) break;
                index++;
            }
            return (index * Pitch + RoomSize / 2f, room.Ring * Pitch + RoomSize / 2f);
        }

        public IReadOnlyList<LevelPlacement> Build()
        {
            var cells = new HashSet<(int, int)>();
            foreach (var room in _floor.Rooms) AddRoom(cells, room.Id);
            foreach (var c in _floor.Corridors) AddCorridor(cells, c.A, c.B);
            var result = new List<LevelPlacement>();
            foreach (var (x, z) in cells) result.Add(new LevelPlacement(VisualId.Suelo, x, 0f, z));
            foreach (var room in _floor.Rooms) AddMarker(result, room);
            return result;
        }

        private void AddRoom(HashSet<(int, int)> cells, int id)
        {
            var (cx, cz) = CenterOf(id);
            for (var x = 0; x < RoomSize; x++)
                for (var z = 0; z < RoomSize; z++)
                    cells.Add(((int)cx - RoomSize / 2 + x, (int)cz - RoomSize / 2 + z));
        }

        private void AddCorridor(HashSet<(int, int)> cells, int a, int b)
        {
            var (ax, az) = CenterOf(a);
            var (bx, bz) = CenterOf(b);
            var step = Math.Sign(bx - ax);
            for (var x = (int)ax; x != (int)bx; x += step) cells.Add((x, (int)az));
            step = Math.Sign(bz - az);
            for (var z = (int)az; z != (int)bz; z += step) cells.Add(((int)bx, z));
        }

        private void AddMarker(List<LevelPlacement> result, FloorRoom room)
        {
            var (x, z) = CenterOf(room.Id);
            if (room.Kind == RoomKind.Entrance) result.Add(new LevelPlacement(VisualId.Ascensor, x, 0f, z + 2f));
            if (room.Kind == RoomKind.EmergencyExit) result.Add(new LevelPlacement(VisualId.Puerta, x, 0f, z));
        }

        /// <summary>Punto de aparición del jugador: junto al Ascensor de la entrada.</summary>
        public (float X, float Z) SpawnPoint => CenterOf(_floor.Entrance.Id);
    }
}
