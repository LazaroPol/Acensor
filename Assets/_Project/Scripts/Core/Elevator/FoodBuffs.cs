using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Stats;
using Specimen.Core.Time;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Buffs de comida activos de un personaje (GDD §6-§7). Comer requiere Cocina construida; la duración
    /// escala con su nivel. Repetir plato renueva la duración sin apilar; platos distintos se acumulan.
    /// </summary>
    public sealed class FoodBuffs : ITickable
    {
        private readonly StatSheet _sheet;
        private readonly IModuleLevels _levels;
        private readonly KitchenConfig _config;
        private readonly Dictionary<Meal, float> _remaining = new Dictionary<Meal, float>();
        private readonly List<Meal> _expired = new List<Meal>();

        /// <summary>Se lanza cuando un buff de comida se agota y se retira.</summary>
        public event Action<Meal> BuffExpired;

        public FoodBuffs(StatSheet sheet, IModuleLevels levels, KitchenConfig config = null)
        {
            _sheet = Guard.NotNull(sheet, nameof(sheet));
            _levels = Guard.NotNull(levels, nameof(levels));
            _config = config ?? KitchenConfig.Default;
        }

        public int KitchenLevel => _levels.LevelOf(ModuleType.Kitchen);

        public int ActiveCount => _remaining.Count;

        public bool IsActive(Meal meal) => meal != null && _remaining.ContainsKey(meal);

        public float RemainingOf(Meal meal) => meal != null && _remaining.TryGetValue(meal, out var s) ? s : 0f;

        /// <summary>Come el plato. Devuelve false si no hay Cocina construida.</summary>
        public bool Eat(Meal meal)
        {
            Guard.NotNull(meal, nameof(meal));
            int level = KitchenLevel;
            if (level <= 0) return false;
            if (!_remaining.ContainsKey(meal)) _sheet.AddModifier(meal.Stat, meal.CreateModifier(meal));
            _remaining[meal] = _config.DurationAt(meal, level);
            return true;
        }

        public void Tick(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            if (deltaSeconds <= 0f || _remaining.Count == 0) return;
            _expired.Clear();
            foreach (var meal in new List<Meal>(_remaining.Keys))
            {
                float left = _remaining[meal] - deltaSeconds;
                if (left > 0f) _remaining[meal] = left;
                else _expired.Add(meal);
            }
            foreach (var meal in _expired) Expire(meal);
        }

        private void Expire(Meal meal)
        {
            _remaining.Remove(meal);
            _sheet.Get(meal.Stat).RemoveAllFromSource(meal);
            BuffExpired?.Invoke(meal);
        }
    }
}
