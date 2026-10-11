using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Events;

namespace Specimen.Tests.Core.TestDoubles
{
    /// <summary>Publicador espía: guarda todos los eventos publicados, en orden.</summary>
    public sealed class RecordingPublisher : IEventPublisher
    {
        public List<object> Published { get; } = new List<object>();

        public void Publish<TEvent>(TEvent evt) => Published.Add(evt);

        public List<TEvent> OfType<TEvent>() => Published.OfType<TEvent>().ToList();
    }
}
