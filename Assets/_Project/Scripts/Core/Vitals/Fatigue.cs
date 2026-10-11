using System;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Vitals
{
    /// <summary>
    /// Fatiga acumulada entre pisos. Cada punto reduce la stamina máxima mediante un único modificador
    /// multiplicativo cuya fuente es esta instancia. Se recupera con la Hoguera o comidas especiales.
    /// </summary>
    public sealed class Fatigue
    {
        private readonly Stat _maxStamina;
        private readonly FatigueConfig _config;
        private StatModifier _modifier;

        public event Action<Fatigue> Changed;

        public Fatigue(Stat maxStamina, FatigueConfig config = null)
        {
            _maxStamina = Guard.NotNull(maxStamina, nameof(maxStamina));
            _config = config ?? FatigueConfig.Default;
        }

        public int Points { get; private set; }
        public int MaxPoints => _config.MaxPoints;
        public bool IsExhausted => Points >= _config.MaxPoints;
        public float StaminaFactor => _config.StaminaFactorAt(Points);

        /// <summary>Acumula la fatiga de completar un piso (con extra si quedó atrapado). Devuelve lo sumado.</summary>
        public int AccumulateFloor(bool wasTrapped = false)
        {
            int amount = _config.PointsPerFloor + (wasTrapped ? _config.TrappedExtraPoints : 0);
            return Add(amount);
        }

        /// <summary>Suma puntos sin pasar del tope y devuelve los sumados de verdad.</summary>
        public int Add(int points)
        {
            Guard.NonNegative(points, nameof(points));
            int before = Points;
            SetPoints(Math.Min(_config.MaxPoints, Points + points));
            return Points - before;
        }

        /// <summary>Recupera puntos (Hoguera, comida) sin bajar de 0 y devuelve los recuperados de verdad.</summary>
        public int Recover(int points)
        {
            Guard.NonNegative(points, nameof(points));
            int before = Points;
            SetPoints(Math.Max(0, Points - points));
            return before - Points;
        }

        public void Clear() => SetPoints(0);

        private void SetPoints(int value)
        {
            if (value == Points) return;
            Points = value;
            if (_modifier != null) _maxStamina.RemoveModifier(_modifier);
            _modifier = Points > 0 ? StatModifier.Multiply(StaminaFactor, this) : null;
            if (_modifier != null) _maxStamina.AddModifier(_modifier);
            Changed?.Invoke(this);
        }
    }
}
