using System;
using System.Collections.Generic;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Avance de la cola: combustible, temporizador y entrega (GDD §17.3).</summary>
    public sealed partial class CraftQueue
    {
        public void Tick(float deltaSeconds)
        {
            if (deltaSeconds < 0f) throw new ArgumentOutOfRangeException(nameof(deltaSeconds));
            float left = deltaSeconds;
            while (true)
            {
                if (_jobs.Count == 0) { State = CraftQueueState.Idle; return; }
                var job = _jobs[0];
                if (job.IsDone)
                {
                    if (!Deliver(job)) { State = CraftQueueState.OutputFull; return; }
                    continue;
                }
                if (left <= Epsilon) { State = CraftQueueState.Working; return; }
                if (!EnsureFuel()) { State = CraftQueueState.NoFuel; return; }
                float step = Math.Min(left, job.Remaining);
                if (_station.HasFuelSlots) step = Math.Min(step, BurnLeft);
                job.Advance(step);
                left -= step;
                if (_station.HasFuelSlots) BurnLeft = Math.Max(0f, BurnLeft - step);
            }
        }

        /// <summary>Si hace falta, quema una unidad de combustible; false si no queda.</summary>
        private bool EnsureFuel()
        {
            if (!_station.HasFuelSlots || BurnLeft > Epsilon) return true;
            foreach (var stack in _station.Fuel.Stacks)
            {
                if (!_fuel.IsFuel(stack.Id)) continue;
                var id = stack.Id;
                _station.Fuel.Remove(id, 1);
                BurnLeft += CraftTiming.BurnTime(_fuel.BurnSeconds(id), _station.Parts.FuelSaving);
                return true;
            }
            return false;
        }

        private bool Deliver(CraftJob job)
        {
            if (job.RolledQuality == Quality.None) job.RolledQuality = _roller.Roll(job.QualityInput);
            if (!TryAddOutputs(job)) return false;
            job.Crafter.Gain(job.Recipe.Xp);
            _station.Parts.Wear(job.Recipe.RequiredParts);
            _jobs.RemoveAt(0);
            Completed?.Invoke(job);
            return true;
        }

        /// <summary>Añade todas las salidas (la principal con la calidad tirada) o ninguna.</summary>
        private bool TryAddOutputs(CraftJob job)
        {
            var added = new List<KeyValuePair<string, int>>();
            var outputs = job.Recipe.Outputs;
            for (int i = 0; i < outputs.Count; i++)
            {
                int n = 0;
                for (int u = 0; u < outputs[i].Quantity; u++)
                    n += _station.Output.Add(Instance(outputs[i].Item, i == 0 ? job.RolledQuality : Quality.None));
                if (n > 0) added.Add(new KeyValuePair<string, int>(outputs[i].Item.Id, n));
                if (n == outputs[i].Quantity) continue;
                foreach (var a in added) _station.Output.Remove(a.Key, a.Value);
                return false;
            }
            return true;
        }

        private ItemInstance Instance(ItemDefinition item, Quality quality) =>
            quality == Quality.None ? ItemInstance.CreateNew(item) : ItemInstance.CreateNew(item, _rules.Grade(quality));
    }
}
