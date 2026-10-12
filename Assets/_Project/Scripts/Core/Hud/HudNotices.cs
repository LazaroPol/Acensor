using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Events;
using Specimen.Core.Iteration;
using Specimen.Core.Time;

namespace Specimen.Core.Hud
{
    /// <summary>
    /// Cola de avisos del HUD: se alimenta de eventos de la iteración (puertas cerrándose, cambio de fase,
    /// nuevo piso) y los hace caducar con el tiempo. Los más urgentes se ordenan primero.
    /// </summary>
    public sealed class HudNotices : ITickable, IDisposable
    {
        public const float DefaultLifetime = 4f;

        private readonly List<(string Text, NoticeSeverity Severity, float Left)> _items =
            new List<(string, NoticeSeverity, float)>();
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        public HudNotices(IEventSubscriber events)
        {
            Guard.NotNull(events, nameof(events));
            _subscriptions.Add(events.Subscribe<DoorsClosingWarning>(e => Add(
                string.Format(CultureInfo.InvariantCulture, "¡Las puertas se cierran en {0} s!", (int)Math.Ceiling(e.SecondsLeft)),
                NoticeSeverity.Danger)));
            _subscriptions.Add(events.Subscribe<IterationPhaseChanged>(e => Add(
                "Fase: " + PhaseLabels.Of(e.To), NoticeSeverity.Info)));
            _subscriptions.Add(events.Subscribe<FloorReached>(e => Add(
                "Piso " + e.Floor, NoticeSeverity.Info)));
        }

        public void Add(string text, NoticeSeverity severity, float lifetimeSeconds = DefaultLifetime) =>
            _items.Add((Guard.NotNull(text, nameof(text)), severity, Guard.Positive(lifetimeSeconds, nameof(lifetimeSeconds))));

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                var item = _items[i];
                item.Left -= deltaSeconds;
                if (item.Left <= 0f) _items.RemoveAt(i);
                else _items[i] = item;
            }
        }

        /// <summary>Avisos vigentes: primero los de mayor gravedad y, a igual gravedad, los más recientes.</summary>
        public IReadOnlyList<HudNotice> Active()
        {
            var result = new List<HudNotice>(_items.Count);
            for (int i = _items.Count - 1; i >= 0; i--)
                result.Add(new HudNotice(_items[i].Text, _items[i].Severity, _items[i].Left));
            return result.OrderByDescending(n => n.Severity).ToList();
        }

        public void Dispose()
        {
            foreach (var s in _subscriptions) s.Dispose();
            _subscriptions.Clear();
        }
    }
}
