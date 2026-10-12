using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Progression;
using Specimen.Core.Randomness;
using Specimen.Core.Visuals;
using Specimen.Core.World;
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
            Hits = new HitResolver(Random, Feed) { DamageBonus = () => Fury.Current };
            Crafter = new Crafter(Recipes);
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
        public HeroProgression Hero { get; } = new HeroProgression();
        public RecipeBook Recipes { get; } = DefaultRecipes.CreateBook();
        public Crafter Crafter { get; }
        public TimedBonus Fury { get; } = new TimedBonus();
        public VillageLayout Village { get; } = new VillageLayout();
        public InteractionHub Interactions { get; set; }

        /// <summary>Lo levanta un banco al usarlo con E; la interfaz abre la ventana de profesión.</summary>
        public event System.Action<ProfessionId> BenchOpened;

        public void OpenBench(ProfessionId profession) => BenchOpened?.Invoke(profession);

        /// <summary>Suma experiencia, la anuncia y devuelve los niveles ganados.</summary>
        public int GrantXp(int amount, string reason)
        {
            int levels = Hero.Progress.AddXp(amount);
            Feed.Add($"+{amount} PX ({reason})");
            return levels;
        }
    }
}
