using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Composition
{
    /// <summary>
    /// Contenedor mínimo de dependencias (sin reflexión). Las fábricas son perezosas y de instancia única;
    /// registrar dos veces el mismo tipo o resolver uno desconocido es un error de composición.
    /// </summary>
    public sealed class ServiceContainer : IServiceRegistry, IServiceResolver
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Func<IServiceResolver, object>> _factories =
            new Dictionary<Type, Func<IServiceResolver, object>>();
        private readonly HashSet<Type> _building = new HashSet<Type>();

        public void Register<T>(T instance) where T : class
        {
            Guard.NotNull(instance, nameof(instance));
            EnsureFree(typeof(T));
            _instances[typeof(T)] = instance;
        }

        public void RegisterFactory<T>(Func<IServiceResolver, T> factory) where T : class
        {
            Guard.NotNull(factory, nameof(factory));
            EnsureFree(typeof(T));
            _factories[typeof(T)] = r => factory(r);
        }

        public T Resolve<T>() where T : class =>
            TryResolve<T>(out var service)
                ? service
                : throw new InvalidOperationException($"Servicio no registrado: {typeof(T).Name}.");

        public bool TryResolve<T>(out T service) where T : class
        {
            var type = typeof(T);
            if (_instances.TryGetValue(type, out var found)) { service = (T)found; return true; }
            if (!_factories.TryGetValue(type, out var factory)) { service = null; return false; }
            if (!_building.Add(type))
                throw new InvalidOperationException($"Dependencia circular al construir {type.Name}.");
            try { service = (T)factory(this); }
            finally { _building.Remove(type); }
            _instances[type] = service;
            _factories.Remove(type);
            return true;
        }

        private void EnsureFree(Type type)
        {
            if (_instances.ContainsKey(type) || _factories.ContainsKey(type))
                throw new InvalidOperationException($"Servicio ya registrado: {type.Name}.");
        }
    }
}
