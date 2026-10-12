using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Common;
using Specimen.Core.Elevator;
using Specimen.Core.Events;
using Specimen.Core.Iteration;
using Specimen.Core.Loot;
using Specimen.Core.Profile;

namespace Specimen.Core.Composition
{
    /// <summary>
    /// Raíz de composición del juego: registra los servicios compartidos por todas las escenas.
    /// Es C# puro (testeable); la capa Unity solo aporta el <see cref="ISceneLoader"/>.
    /// </summary>
    public static class GameComposition
    {
        /// <summary>Jugador local (partida individual hasta que llegue la red).</summary>
        public const int LocalPlayerId = 1;

        /// <summary>Ritmo de la demo: un solo piso (Calma 20 s, Incursión 2,5 min) y después el Ático.</summary>
        public static IterationConfig DemoConfig =>
            new IterationConfigBuilder().WithFloors(1).WithCalm(20f).WithIncursion(150f).WithDoorWarnings(60f, 15f).Build();

        /// <summary>Compone los servicios. Sin <paramref name="profileStore"/>, el perfil vive solo en memoria.</summary>
        public static ServiceContainer Build(ISceneLoader loader, IProfileStore profileStore = null)
        {
            Guard.NotNull(loader, nameof(loader));
            var container = new ServiceContainer();
            container.Register<IProfileStore>(profileStore ?? new InMemoryProfileStore());
            container.RegisterFactory(r => PlayerProfile.Load(r.Resolve<IProfileStore>()));
            container.Register(new RunHaul());
            container.Register<IEventBus>(new EventBus());
            container.Register(loader);
            container.RegisterFactory(r => new SceneFlow(r.Resolve<ISceneLoader>(), r.Resolve<IEventBus>()));
            container.Register<IClassKitCatalog>(ClassKitCatalog.CreateDefault());
            container.RegisterFactory(r => new ClassKitArmory(r.Resolve<IClassKitCatalog>()));
            container.RegisterFactory(r => new IterationRun(DemoConfig, r.Resolve<IEventBus>(), new[] { LocalPlayerId }));
            container.RegisterFactory(_ => new ElevatorPlatform(4, new BlueprintCatalog(new ModuleBlueprint[0])));
            return container;
        }
    }
}
