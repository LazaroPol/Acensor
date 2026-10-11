using System;

namespace Specimen.Core.Events
{
    /// <summary>Publica eventos de dominio. Segregado del suscriptor (ISP): quien publica no necesita suscribirse.</summary>
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent evt);
    }

    /// <summary>Se suscribe a eventos de dominio. Desechar el resultado cancela la suscripción.</summary>
    public interface IEventSubscriber
    {
        IDisposable Subscribe<TEvent>(Action<TEvent> handler);
    }

    public interface IEventBus : IEventPublisher, IEventSubscriber
    {
    }
}
