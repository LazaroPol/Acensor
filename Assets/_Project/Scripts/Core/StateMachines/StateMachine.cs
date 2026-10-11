using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Time;

namespace Specimen.Core.StateMachines
{
    /// <summary>
    /// Máquina de estados genérica con transiciones condicionales.
    /// En cada Tick: primero se evalúan las transiciones "desde cualquier estado", luego las del estado actual
    /// (en orden de registro); si ninguna se cumple, se hace Tick del estado actual.
    /// Abierta a extensión (nuevos estados/transiciones) sin modificar la clase (OCP).
    /// </summary>
    public sealed class StateMachine<TStateId> : ITickable where TStateId : struct, Enum
    {
        private readonly Dictionary<TStateId, IState> _states = new Dictionary<TStateId, IState>();
        private readonly Dictionary<TStateId, List<Transition>> _transitions = new Dictionary<TStateId, List<Transition>>();
        private readonly List<Transition> _anyTransitions = new List<Transition>();
        private bool _changing;

        public TStateId CurrentId { get; private set; }
        public bool IsRunning { get; private set; }

        /// <summary>(desde, hacia). Se invoca tras Enter del nuevo estado.</summary>
        public event Action<TStateId, TStateId> StateChanged;

        public StateMachine<TStateId> AddState(TStateId id, IState state)
        {
            if (_states.ContainsKey(id)) throw new InvalidOperationException($"El estado {id} ya existe.");
            _states[id] = Guard.NotNull(state, nameof(state));
            return this;
        }

        public StateMachine<TStateId> AddTransition(TStateId from, TStateId to, Func<bool> condition)
        {
            EnsureExists(from);
            EnsureExists(to);
            if (!_transitions.TryGetValue(from, out var list))
                _transitions[from] = list = new List<Transition>();
            list.Add(new Transition(to, Guard.NotNull(condition, nameof(condition))));
            return this;
        }

        public StateMachine<TStateId> AddAnyTransition(TStateId to, Func<bool> condition)
        {
            EnsureExists(to);
            _anyTransitions.Add(new Transition(to, Guard.NotNull(condition, nameof(condition))));
            return this;
        }

        public void Start(TStateId initial)
        {
            if (IsRunning) throw new InvalidOperationException("La máquina ya está en marcha.");
            EnsureExists(initial);
            IsRunning = true;
            CurrentId = initial;
            _states[initial].Enter();
        }

        public void ChangeState(TStateId to)
        {
            if (!IsRunning) throw new InvalidOperationException("La máquina no se ha iniciado.");
            EnsureExists(to);
            if (_changing) throw new InvalidOperationException("No se puede cambiar de estado durante Enter/Exit.");

            _changing = true;
            try
            {
                var from = CurrentId;
                _states[from].Exit();
                CurrentId = to;
                _states[to].Enter();
                StateChanged?.Invoke(from, to);
            }
            finally
            {
                _changing = false;
            }
        }

        public void Tick(float deltaSeconds)
        {
            if (!IsRunning) return;

            foreach (var t in _anyTransitions)
            {
                if (!EqualityComparer<TStateId>.Default.Equals(t.To, CurrentId) && t.Condition())
                {
                    ChangeState(t.To);
                    return;
                }
            }

            if (_transitions.TryGetValue(CurrentId, out var list))
            {
                foreach (var t in list)
                {
                    if (t.Condition())
                    {
                        ChangeState(t.To);
                        return;
                    }
                }
            }

            _states[CurrentId].Tick(deltaSeconds);
        }

        private void EnsureExists(TStateId id)
        {
            if (!_states.ContainsKey(id)) throw new InvalidOperationException($"El estado {id} no está registrado.");
        }

        private readonly struct Transition
        {
            public readonly TStateId To;
            public readonly Func<bool> Condition;

            public Transition(TStateId to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
    }
}
