using System;
using Specimen.Core.Common;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Durabilidad acotada a [0, Max]. Al llegar a 0 el objeto queda roto (inutilizable)
    /// hasta que se repara. Con Max = 0 el objeto es indestructible y nunca cambia.
    /// </summary>
    public sealed class Durability : IDurability
    {
        public int Current { get; private set; }
        public int Max { get; }
        public bool IsIndestructible => Max == 0;
        public bool IsBroken => !IsIndestructible && Current == 0;
        public float Ratio => IsIndestructible ? 1f : (float)Current / Max;

        public event Action<int> Changed;
        public event Action Broken;

        public Durability(int max) : this(max, max) { }

        public Durability(int max, int current)
        {
            Max = Guard.NonNegative(max, nameof(max));
            Guard.NonNegative(current, nameof(current));
            if (current > max) throw new ArgumentOutOfRangeException(nameof(current), current, "Supera el máximo.");
            Current = current;
        }

        public int Wear(int amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            if (IsIndestructible || IsBroken) return 0;
            var lost = Math.Min(amount, Current);
            if (lost == 0) return 0;
            Current -= lost;
            Changed?.Invoke(Current);
            if (Current == 0) Broken?.Invoke();
            return lost;
        }

        public int Repair(int amount)
        {
            Guard.NonNegative(amount, nameof(amount));
            var gained = Math.Min(amount, Max - Current);
            if (gained == 0) return 0;
            Current += gained;
            Changed?.Invoke(Current);
            return gained;
        }
    }
}
