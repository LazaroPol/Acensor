using System;

namespace Specimen.Core.Composition
{
    /// <summary>Registra servicios por tipo (ISP: quien compone no resuelve y viceversa).</summary>
    public interface IServiceRegistry
    {
        void Register<T>(T instance) where T : class;
        void RegisterFactory<T>(Func<IServiceResolver, T> factory) where T : class;
    }

    /// <summary>Resuelve servicios ya registrados.</summary>
    public interface IServiceResolver
    {
        T Resolve<T>() where T : class;
        bool TryResolve<T>(out T service) where T : class;
    }
}
