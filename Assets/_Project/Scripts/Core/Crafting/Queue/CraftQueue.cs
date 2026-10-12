using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Time;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>
    /// Cola de fabricación de una <see cref="Workstation"/> (GDD §17.3): al encolar reserva y retira los
    /// materiales (entrada de la estación y, si faltan, bolsa del jugador); avanza con el tiempo,
    /// quema combustible, tira calidad, entrega a la salida y da XP a quien encoló.
    /// </summary>
    public sealed partial class CraftQueue : ITickable
    {
        internal const float Epsilon = 1e-4f;

        private readonly List<CraftJob> _jobs = new List<CraftJob>();
        private readonly Workstation _station;
        private readonly IQualityRoller _roller;
        private readonly QualityRules _rules;
        private readonly FuelTable _fuel;
        private readonly ProfessionMilestones _milestones;

        public CraftQueue(Workstation station, IQualityRoller roller, QualityRules rules = null,
            FuelTable fuel = null, ProfessionMilestones milestones = null)
        {
            _station = Guard.NotNull(station, nameof(station));
            _roller = Guard.NotNull(roller, nameof(roller));
            _rules = rules ?? QualityRules.Default;
            _fuel = fuel ?? FuelTable.Default;
            _milestones = milestones ?? ProfessionMilestones.Default;
        }

        /// <summary>Se lanza al entregar un trabajo terminado en la salida.</summary>
        public event Action<CraftJob> Completed;

        public IReadOnlyList<CraftJob> Jobs => _jobs;
        public CraftQueueState State { get; private set; } = CraftQueueState.Idle;

        /// <summary>Segundos de fuego que quedan de la última unidad de combustible.</summary>
        public float BurnLeft { get; private set; }

        public EnqueueResult Enqueue(RecipeDefinition recipe, ProfessionSkill crafter, IInventory bag = null,
            IReadOnlyList<IngredientChoice> choices = null)
        {
            Guard.NotNull(recipe, nameof(recipe));
            Guard.NotNull(crafter, nameof(crafter));
            if (recipe.Station != _station.Type) return EnqueueResult.WrongStation;
            if (_jobs.Count >= _station.QueueSize) return EnqueueResult.QueueFull;
            var source = new InventoryChain(_station.Input, bag);
            var plan = TagConsumptionPlanner.Plan(recipe.Inputs, source, choices);
            if (!plan.IsComplete) return EnqueueResult.MissingMaterials;
            foreach (var e in plan.Entries) source.Remove(e.Item.Id, e.Quantity);
            _jobs.Add(CreateJob(recipe, crafter, plan, source));
            if (State == CraftQueueState.Idle) State = CraftQueueState.Working;
            return EnqueueResult.Queued;
        }

        /// <summary>Cancela un trabajo que aún no ha empezado y devuelve íntegros sus materiales.</summary>
        public bool Cancel(CraftJob job)
        {
            if (job == null || job.IsStarted || !_jobs.Contains(job)) return false;
            if (!Refund(job)) return false;
            _jobs.Remove(job);
            if (_jobs.Count == 0) State = CraftQueueState.Idle;
            return true;
        }

        private CraftJob CreateJob(RecipeDefinition recipe, ProfessionSkill crafter, ConsumptionPlan plan,
            IInventory source)
        {
            var milestone = _milestones.For(crafter.Level);
            var quality = new QualityRollInput(crafter.Level, plan.AverageTier, _station.Level,
                _station.QualityBonus, milestone.MaxQuality, recipe.QualityCap);
            var duration = CraftTiming.Duration(recipe.CraftSeconds, milestone.TimeBonus, _station.Parts.TimeBonus);
            return new CraftJob(recipe, crafter, plan.Entries, source, quality, duration);
        }

        private static bool Refund(CraftJob job)
        {
            var added = new List<PlannedConsumption>();
            foreach (var e in job.Taken)
            {
                int n = job.Source.Add(ItemInstance.CreateNew(e.Item), e.Quantity);
                if (n > 0) added.Add(new PlannedConsumption(e.Item, n));
                if (n == e.Quantity) continue;
                foreach (var a in added) job.Source.Remove(a.Item.Id, a.Quantity);
                return false;
            }
            return true;
        }
    }
}
