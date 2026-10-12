using Specimen.Core.Common;
using Specimen.Core.Events;

namespace Specimen.Core.Composition
{
    /// <summary>
    /// Raíz de composición del juego: registra los servicios compartidos por todas las escenas.
    /// Es C# puro (testeable); la capa Unity solo aporta el <see cref="ISceneLoader"/>.
    /// </summary>
    public static class GameComposition
    {
        public static ServiceContainer Build(ISceneLoader loader)
        {
            Guard.NotNull(loader, nameof(loader));
            var container = new ServiceContainer();
            container.Register<IEventBus>(new EventBus());
            container.Register(loader);
            container.RegisterFactory(r => new SceneFlow(r.Resolve<ISceneLoader>(), r.Resolve<IEventBus>()));
            return container;
        }
    }
}
