using System;

namespace Specimen.Core.StateMachines
{
    /// <summary>Transición condicional hacia un estado destino.</summary>
    internal readonly struct Transition<TStateId> where TStateId : struct, Enum
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
