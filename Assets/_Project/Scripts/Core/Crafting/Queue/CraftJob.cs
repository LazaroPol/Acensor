using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>
    /// Trabajo encolado: receta, materiales ya retirados, de dónde salieron, quién lo encoló y los
    /// datos de calidad congelados al encolar (GDD §17.3).
    /// </summary>
    public sealed class CraftJob
    {
        internal CraftJob(RecipeDefinition recipe, ProfessionSkill crafter, IReadOnlyList<PlannedConsumption> taken,
            IInventory source, QualityRollInput quality, float duration)
        {
            Recipe = Guard.NotNull(recipe, nameof(recipe));
            Crafter = Guard.NotNull(crafter, nameof(crafter));
            Taken = Guard.NotNull(taken, nameof(taken));
            Source = Guard.NotNull(source, nameof(source));
            QualityInput = Guard.NotNull(quality, nameof(quality));
            Duration = duration;
        }

        public RecipeDefinition Recipe { get; }

        /// <summary>Oficio de quien encoló: recibe la XP al terminar.</summary>
        public ProfessionSkill Crafter { get; }

        public IReadOnlyList<PlannedConsumption> Taken { get; }
        internal IInventory Source { get; }
        public QualityRollInput QualityInput { get; }
        public float Duration { get; }
        public float Elapsed { get; private set; }

        /// <summary>Calidad ya tirada (se guarda para no repetir la tirada si la salida está llena).</summary>
        public Quality RolledQuality { get; internal set; }

        public bool IsStarted => Elapsed > 0f;
        public bool IsDone => Elapsed >= Duration;
        public float Remaining => Math.Max(0f, Duration - Elapsed);
        public float Progress => Duration <= 0f ? 1f : Math.Min(1f, Elapsed / Duration);

        internal void Advance(float seconds) =>
            Elapsed = Elapsed + seconds >= Duration - CraftQueue.Epsilon ? Duration : Elapsed + seconds;

        public override string ToString() => $"{Recipe.Id} {Elapsed:0.#}/{Duration:0.#}s";
    }
}
