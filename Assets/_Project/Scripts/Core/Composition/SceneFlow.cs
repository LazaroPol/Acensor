using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Events;

namespace Specimen.Core.Composition
{
    /// <summary>Evento: la partida cambió de escena.</summary>
    public readonly struct SceneChanged
    {
        public SceneChanged(SceneId from, SceneId to) { From = from; To = to; }
        public SceneId From { get; }
        public SceneId To { get; }
    }

    /// <summary>
    /// Flujo de escenas: solo permite las transiciones del bucle de juego
    /// (Boot → Ascensor ⇄ Armería/Laboratorio, Ascensor ⇄ Incursión/Ático) y avisa por el bus.
    /// </summary>
    public sealed class SceneFlow
    {
        private static readonly Dictionary<SceneId, SceneId[]> Allowed = new Dictionary<SceneId, SceneId[]>
        {
            [SceneId.Boot] = new[] { SceneId.Ascensor },
            [SceneId.Ascensor] = new[] { SceneId.Armeria, SceneId.Laboratorio, SceneId.Incursion, SceneId.Atico },
            [SceneId.Armeria] = new[] { SceneId.Ascensor },
            [SceneId.Laboratorio] = new[] { SceneId.Ascensor },
            [SceneId.Incursion] = new[] { SceneId.Ascensor },
            [SceneId.Atico] = new[] { SceneId.Ascensor }
        };

        private readonly ISceneLoader _loader;
        private readonly IEventPublisher _events;

        public SceneFlow(ISceneLoader loader, IEventPublisher events)
        {
            _loader = Guard.NotNull(loader, nameof(loader));
            _events = Guard.NotNull(events, nameof(events));
        }

        public SceneId Current { get; private set; } = SceneId.Boot;

        public bool CanGoTo(SceneId target) => System.Array.IndexOf(Allowed[Current], target) >= 0;

        /// <summary>Cambia de escena si la transición es válida; devuelve false (sin efectos) si no.</summary>
        public bool GoTo(SceneId target)
        {
            if (!CanGoTo(target)) return false;
            var from = Current;
            Current = target;
            _loader.Load(target);
            _events.Publish(new SceneChanged(from, target));
            return true;
        }
    }
}
