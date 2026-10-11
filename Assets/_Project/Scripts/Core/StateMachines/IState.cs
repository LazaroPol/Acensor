using Specimen.Core.Time;

namespace Specimen.Core.StateMachines
{
    /// <summary>Estado de una máquina de estados. Cada estado es una clase con una sola responsabilidad (SRP).</summary>
    public interface IState : ITickable
    {
        void Enter();
        void Exit();
    }

    /// <summary>Base con implementaciones vacías para que cada estado solo sobrescriba lo que necesita.</summary>
    public abstract class StateBase : IState
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Tick(float deltaSeconds) { }
    }
}
