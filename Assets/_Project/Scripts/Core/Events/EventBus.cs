using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Events
{
    /// <summary>
    /// Bus de eventos tipado y síncrono. Desacopla sistemas (p. ej. el temporizador de puertas y el HUD).
    /// - Es seguro (des)suscribirse durante un Publish: se itera sobre una copia.
    /// - Si un manejador lanza, el resto se ejecuta igualmente y luego se relanza un AggregateException.
    /// </summary>
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();

        public IDisposable Subscribe<TEvent>(Action<TEvent> handler)
        {
            Guard.NotNull(handler, nameof(handler));
            if (!_handlers.TryGetValue(typeof(TEvent), out var list))
            {
                list = new List<Delegate>();
                _handlers[typeof(TEvent)] = list;
            }
            list.Add(handler);
            return new Subscription(() => list.Remove(handler));
        }

        public void Publish<TEvent>(TEvent evt)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out var list) || list.Count == 0) return;

            List<Exception> errors = null;
            foreach (var handler in list.ToArray())
            {
                try
                {
                    ((Action<TEvent>)handler)(evt);
                }
                catch (Exception ex)
                {
                    (errors ??= new List<Exception>()).Add(ex);
                }
            }
            if (errors != null) throw new AggregateException(errors);
        }

        public int SubscriberCount<TEvent>() =>
            _handlers.TryGetValue(typeof(TEvent), out var list) ? list.Count : 0;

        private sealed class Subscription : IDisposable
        {
            private Action _dispose;

            public Subscription(Action dispose) => _dispose = dispose;

            public void Dispose()
            {
                _dispose?.Invoke();
                _dispose = null;
            }
        }
    }
}
