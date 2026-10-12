using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Session;
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
    public sealed partial class SandboxContext
    {
        public const int LocalPlayer = 1;

        public SandboxContext(CatalogoArte art, IVisualCatalog<GameObject> visuals, SandboxInput input, ulong seed)
        {
            Art = art;
            Visuals = visuals;
            Input = input;
            Random = new Pcg32Random(seed);
            Feed = new CombatFeed();
            Hits = new HitResolver(Random, Feed, Events) { DamageBonus = () => Fury.Current, PlayerLevel = () => Hero.Level };
            Crafter = new Crafter(Recipes);
            Crafting = new CraftingProfile(Inventory, curve: CraftingData.Curve);
            Stations = new StationSessionFactory(CraftingData, Crafting, Random);
        }

        public CatalogoArte Art { get; }
        public IVisualCatalog<GameObject> Visuals { get; }
        public SandboxInput Input { get; }
        public IRandom Random { get; }
        public CombatFeed Feed { get; }
        public HitResolver Hits { get; }
        public CombatEvents Events { get; } = new CombatEvents();
        public EnemyRegistry Enemies { get; } = new EnemyRegistry();
        public Inventory Inventory { get; } = new Inventory(24);
        public ProfessionBench Bench { get; } = new ProfessionBench(SynergyCatalog.CreateDefault());
        public HeroProgression Hero { get; } = new HeroProgression();
        public RecipeBook Recipes { get; } = DefaultRecipes.CreateBook();
        public Crafter Crafter { get; }
        public ICraftingCatalog CraftingData { get; } = new CraftingCatalog(DefaultCraftingData.Create());
        public CraftingProfile Crafting { get; }
        public StationSessionFactory Stations { get; }
        public TimedBonus Fury { get; } = new TimedBonus();
        public VillageLayout Village { get; } = new VillageLayout();
        public InteractionHub Interactions { get; set; }
        public MouseGestures Mouse { get; set; }

        /// <summary>Lo levanta una estación al usarla con E; la interfaz abre su ventana.</summary>
        public event System.Action<BenchStation> BenchOpened;

        public void OpenBench(BenchStation bench) => BenchOpened?.Invoke(bench);

        /// <summary>Trabajo de estación terminado: XP de oficio en el HUD (color de profesión) y en el registro.</summary>
        public void ReportCraft(CraftCompletion done)
        {
            Events.Xp(done.Xp, XpSource.Profesion);
            string trade = StationLabels.Profession(done.Profession);
            Feed.Add($"Terminado: {done.Job.Recipe.MainOutput.Item.Name}. +{done.Xp} XP de {trade}" +
                     (done.LevelsGained > 0 ? $" · ¡{trade} nivel {done.Level}!" : string.Empty));
        }

        /// <summary>Suma experiencia, la anuncia (azul claro combate, verde profesión) y devuelve los niveles ganados.</summary>
        public int GrantXp(int amount, string reason, XpSource source)
        {
            Events.Xp(amount, source);
            int levels = Hero.Progress.AddXp(amount);
            Feed.Add($"+{amount} PX ({reason})");
            return levels;
        }
    }
}
