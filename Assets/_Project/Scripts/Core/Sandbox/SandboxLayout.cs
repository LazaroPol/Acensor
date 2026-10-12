using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Bestiary;
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

        public IReadOnlyList<LevelPlacement> Room()
            => new GreyboxRoomLayout(Width, Depth).Build().Where(p => p.Id != VisualId.Jugador).ToList();

        public SandboxSpot PlayerSpawn => new SandboxSpot("jugador", Width / 2f, 3f);

        public IReadOnlyList<SandboxSpot> Enemies()
        {
            var bestiary = DefaultBestiary.All();
            var spots = new List<SandboxSpot>();
            for (int i = 0; i < bestiary.Count; i++)
            {
                float x = 4f + i * (Width - 8f) / Math.Max(1, bestiary.Count - 1);
                float z = i % 2 == 0 ? Depth - 4f : Depth - 6.5f;
                spots.Add(new SandboxSpot(bestiary[i].Id, x, z, 180f));
            }
            return spots;
        }

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
