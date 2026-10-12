using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Common;
using Specimen.Core.Elevator;
using Specimen.Core.Events;
using Specimen.Core.Iteration;

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

        public static ServiceContainer Build(ISceneLoader loader)
        {
            Guard.NotNull(loader, nameof(loader));
            var container = new ServiceContainer();
            container.Register<IEventBus>(new EventBus());
            container.Register(loader);
            container.RegisterFactory(r => new SceneFlow(r.Resolve<ISceneLoader>(), r.Resolve<IEventBus>()));
            container.Register<IClassKitCatalog>(ClassKitCatalog.CreateDefault());
            container.RegisterFactory(r => new ClassKitArmory(r.Resolve<IClassKitCatalog>()));
            container.RegisterFactory(r => new IterationRun(IterationConfigBuilder.Default, r.Resolve<IEventBus>(), new[] { LocalPlayerId }));
            container.RegisterFactory(_ => new ElevatorPlatform(4, new BlueprintCatalog(new ModuleBlueprint[0])));
            return container;
        }
    }
}
