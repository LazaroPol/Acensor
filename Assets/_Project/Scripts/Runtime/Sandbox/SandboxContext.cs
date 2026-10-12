using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Randomness;
using Specimen.Core.Visuals;
using Specimen.Runtime.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Servicios compartidos del Campo de pruebas (se crean una vez en el bootstrap y se inyectan).</summary>
    public sealed class SandboxContext
    {
        public const int LocalPlayer = 1;

        public SandboxContext(CatalogoArte art, IVisualCatalog<GameObject> visuals, SandboxInput input, ulong seed)
        {
            Art = art;
            Visuals = visuals;
            Input = input;
            Random = new Pcg32Random(seed);
            Feed = new CombatFeed();
            Hits = new HitResolver(Random, Feed);
        }

        public CatalogoArte Art { get; }
        public IVisualCatalog<GameObject> Visuals { get; }
        public SandboxInput Input { get; }
        public IRandom Random { get; }
        public CombatFeed Feed { get; }
        public HitResolver Hits { get; }
        public EnemyRegistry Enemies { get; } = new EnemyRegistry();
        public Inventory Inventory { get; } = new Inventory(24);
        public ProfessionBench Bench { get; } = new ProfessionBench(SynergyCatalog.CreateDefault());
    }
}
