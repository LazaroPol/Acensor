using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Stats
{
    /// <summary>Atributo con valor base y modificadores; cachea el valor final hasta que cambia algo.</summary>
    public sealed class Stat
    {
        private readonly List<StatModifier> _modifiers = new List<StatModifier>();
        private readonly IStatFormula _formula;
        private float _baseValue;
        private float _cached;
        private bool _dirty = true;

        public event Action<Stat> Changed;

        public Stat(float baseValue, IStatFormula formula = null)
        {
            _baseValue = Guard.NonNegative(baseValue, nameof(baseValue));
            _formula = formula ?? StandardStatFormula.Instance;
        }

        public float BaseValue
        {
            get => _baseValue;
            set { _baseValue = Guard.NonNegative(value, nameof(value)); MarkDirty(); }
        }

        public IReadOnlyList<StatModifier> Modifiers => _modifiers;

        public float Value
        {
            get
            {
                if (_dirty) { _cached = _formula.Evaluate(_baseValue, _modifiers); _dirty = false; }
                return _cached;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(Guard.NotNull(modifier, nameof(modifier)));
            MarkDirty();
        }

        public bool RemoveModifier(StatModifier modifier)
        {
            bool removed = _modifiers.Remove(modifier);
            if (removed) MarkDirty();
            return removed;
        }

        public int RemoveAllFromSource(object source)
        {
            Guard.NotNull(source, nameof(source));
            int removed = _modifiers.RemoveAll(m => Equals(m.Source, source));
            if (removed > 0) MarkDirty();
            return removed;
        }

        private void MarkDirty()
        {
            _dirty = true;
            Changed?.Invoke(this);
        }
    }
}
