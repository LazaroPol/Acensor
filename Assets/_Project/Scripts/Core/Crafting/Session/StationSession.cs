using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Time;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>
    /// Una estación en uso (GDD §17.3, §17.7): une la <see cref="Workstation"/>, su <see cref="CraftQueue"/>
    /// y el <see cref="CraftingProfile"/> del jugador. Es lo que la ventana de estación lee y manda.
    /// </summary>
    public sealed partial class StationSession : ITickable
    {
        private readonly RecipeAvailability _availability;
        private readonly Dictionary<ProfessionId, int> _levels = new Dictionary<ProfessionId, int>();

        public StationSession(Workstation station, CraftQueue queue, RecipeAvailability availability,
            CraftingProfile profile, StationServices services = null)
        {
            Station = Guard.NotNull(station, nameof(station));
            Queue = Guard.NotNull(queue, nameof(queue));
            _availability = Guard.NotNull(availability, nameof(availability));
            Profile = Guard.NotNull(profile, nameof(profile));
            _services = services ?? StationServices.Default;
            Queue.Completed += OnCompleted;
        }

        /// <summary>Trabajo entregado: la XP ya está sumada al oficio de quien lo encoló.</summary>
        public event Action<CraftCompletion> Completed;

        public Workstation Station { get; }
        public CraftQueue Queue { get; }
        public CraftingProfile Profile { get; }

        /// <summary>De dónde salen los materiales: primero la entrada de la estación, luego la bolsa.</summary>
        public IInventory Materials => new InventoryChain(Station.Input, Profile.Bag);

        public IReadOnlyList<RecipeOption> Options() =>
            _availability.For(Station, Profile.Knowledge, Profile.LevelOf, Materials);

        public StationCraftResult Craft(RecipeDefinition recipe)
        {
            Guard.NotNull(recipe, nameof(recipe));
            if (!RecipeAvailability.IsVisible(recipe, Station, Profile.Knowledge)) return StationCraftResult.NotAvailable;
            var block = RecipeAvailability.BlockOf(recipe, Profile.LevelOf, Materials);
            if (block == RecipeBlockReason.ProfessionLevel) return StationCraftResult.ProfessionLevel;
            if (block == RecipeBlockReason.MissingMaterials) return StationCraftResult.MissingMaterials;
            var skill = Profile.Skill(recipe.Profession);
            if (!_levels.ContainsKey(skill.Profession)) _levels[skill.Profession] = skill.Level;
            switch (Queue.Enqueue(recipe, skill, Profile.Bag))
            {
                case EnqueueResult.Queued: return StationCraftResult.Queued;
                case EnqueueResult.QueueFull: return StationCraftResult.QueueFull;
                case EnqueueResult.MissingMaterials: return StationCraftResult.MissingMaterials;
                default: return StationCraftResult.NotAvailable;
            }
        }

        public void Tick(float deltaSeconds) => Queue.Tick(deltaSeconds);

        /// <summary>Pasa a la bolsa todo lo que quepa de la salida; devuelve las unidades movidas.</summary>
        public int CollectOutput()
        {
            int moved = 0;
            foreach (var stack in Station.Output.Stacks.ToList())
            {
                if (Profile.Bag.SpaceFor(stack.Item) < stack.Quantity) continue;
                Profile.Bag.Add(stack.Item, stack.Quantity);
                Station.Output.RemoveInstance(stack.Item);
                moved += stack.Quantity;
            }
            return moved;
        }

        private void OnCompleted(CraftJob job)
        {
            var skill = job.Crafter;
            int before = _levels.TryGetValue(skill.Profession, out var level) ? level : skill.Level;
            _levels[skill.Profession] = skill.Level;
            Completed?.Invoke(new CraftCompletion(job, skill.Level - before));
        }
    }
}
