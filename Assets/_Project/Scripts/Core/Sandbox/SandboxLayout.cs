using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Professions;
using Specimen.Core.Prototype;
using Specimen.Core.Visuals;
using Specimen.Core.Visuals.Packs;

namespace Specimen.Core.Sandbox
{
    /// <summary>
    /// Campo de pruebas: sala cerrada con el jugador al sur, el bestiario al norte, los bancos de
    /// profesión en los muros este y oeste y algo de decorado en las esquinas.
    /// </summary>
    public sealed class SandboxLayout
    {
        public const int Width = 22;
        public const int Depth = 18;

        /// <summary>Celdas del muro sur que se abren como portón hacia el poblado (3 m de hueco).</summary>
        public const int GateFirstCell = Width / 2 - 1, GateLastCell = Width / 2 + 1;

        /// <summary>Sala sin el jugador y con el muro sur abierto en el portón (la puerta 1 m no deja pasar).</summary>
        public IReadOnlyList<LevelPlacement> Room()
            => new GreyboxRoomLayout(Width, Depth).Build()
                .Where(p => p.Id != VisualId.Jugador && !IsGateCell(p)).ToList();

        /// <summary>Arco del portón sur (centro del hueco).</summary>
        public SandboxSpot Gate => new SandboxSpot("arco", (GateFirstCell + GateLastCell + 1) / 2f, 0f);

        private static bool IsGateCell(LevelPlacement p)
            => p.Z == 0f && p.YawDegrees == 0f && p.Id != VisualId.Suelo && p.X >= GateFirstCell && p.X <= GateLastCell;

        public SandboxSpot PlayerSpawn => new SandboxSpot("jugador", Width / 2f, 3f);

        /// <summary>Criaturas dentro de la Armería: ninguna (el bestiario vive en el poblado, ver VillageLayout).</summary>
        public IReadOnlyList<SandboxSpot> Enemies() => new SandboxSpot[0];

        public IReadOnlyList<SandboxSpot> Benches()
        {
            var professions = (ProfessionId[])Enum.GetValues(typeof(ProfessionId));
            var spots = new List<SandboxSpot>();
            for (int i = 0; i < professions.Length; i++)
            {
                bool west = i % 2 == 0;
                float z = 3f + (i / 2) * 3f;
                spots.Add(new SandboxSpot(professions[i].ToString(), west ? 1.5f : Width - 1.5f, z, west ? 90f : 270f));
            }
            return spots;
        }

        public IReadOnlyList<SandboxSpot> Decor()
        {
            var corners = new[] { (6.5f, 9f), (Width - 6.5f, 9f), (Width / 2f - 3f, 6.5f), (Width / 2f + 3f, 6.5f) };
            return corners.Select((c, i) => new SandboxSpot(EnvironmentKit.Decor[i % EnvironmentKit.Decor.Count],
                c.Item1, c.Item2, i * 90f)).ToList();
        }
    }
}
