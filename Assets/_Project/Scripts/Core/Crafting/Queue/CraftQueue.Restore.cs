using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Vuelve a poner en cola un trabajo guardado con los segundos que le quedaban (GDD §17.7).</summary>
    public sealed partial class CraftQueue
    {
        /// <summary>Margen para que un trabajo restaurado cuente como empezado (sus materiales ya se gastaron).</summary>
        private const float RestoredStart = 0.001f;

        /// <summary>
        /// Restaura un trabajo sin consumir materiales: ya se pagaron antes de guardar, así que no se puede
        /// cancelar. Devuelve null si la receta no es de esta estación o la cola está llena.
        /// </summary>
        public CraftJob Restore(RecipeDefinition recipe, ProfessionSkill crafter, float remainingSeconds,
            IInventory bag = null)
        {
            Guard.NotNull(recipe, nameof(recipe));
            Guard.NotNull(crafter, nameof(crafter));
            if (recipe.Station != _station.Type || _jobs.Count >= _station.QueueSize) return null;
            var milestone = _milestones.For(crafter.Level);
            var quality = new QualityRollInput(crafter.Level, 1f, _station.Level, _station.QualityBonus,
                milestone.MaxQuality, recipe.QualityCap);
            var duration = CraftTiming.Duration(recipe.CraftSeconds, milestone.TimeBonus, _station.Parts.TimeBonus);
            var job = new CraftJob(recipe, crafter, new List<PlannedConsumption>(),
                new InventoryChain(_station.Input, bag), quality, duration);
            job.Advance(Math.Max(RestoredStart, duration - Math.Max(0f, remainingSeconds)));
            _jobs.Add(job);
            if (State == CraftQueueState.Idle) State = CraftQueueState.Working;
            return job;
        }
    }
}
