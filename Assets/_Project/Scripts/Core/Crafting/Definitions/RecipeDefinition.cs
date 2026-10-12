using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>
    /// Receta de oficio inmutable (GDD §17.2): entradas, salidas, estación y nivel, piezas exigidas,
    /// oficio y nivel, tiempo, XP, condición de desbloqueo, calidad máxima y variante que mejora.
    /// </summary>
    public sealed partial class RecipeDefinition
    {
        public const int MinStationLevelValue = 1;
        public const int MaxStationLevelValue = 3;

        public string Id { get; }
        public IReadOnlyList<IngredientSpec> Inputs { get; }
        public IReadOnlyList<RecipeOutput> Outputs { get; }
        public StationType Station { get; }
        public int MinStationLevel { get; }
        public IReadOnlyList<string> RequiredParts { get; }
        public ProfessionId Profession { get; }
        public int MinProfessionLevel { get; }
        public float CraftSeconds { get; }
        public int Xp { get; }
        public UnlockCondition Unlock { get; }
        public Quality QualityCap { get; }

        /// <summary>Id de la receta básica que esta variante mejora; null si no es una variante.</summary>
        public string Improves { get; }

        public RecipeOutput MainOutput => Outputs[0];
        public bool IsInstant => CraftSeconds == 0f;
        public bool IsVariant => Improves != null;

        public RecipeDefinition(string id, ProfessionId profession, StationType station,
            IEnumerable<IngredientSpec> inputs, IEnumerable<RecipeOutput> outputs,
            int minStationLevel = MinStationLevelValue, IEnumerable<string> requiredParts = null,
            int minProfessionLevel = 1, float craftSeconds = 0f, int xp = 0,
            UnlockCondition unlock = null, Quality qualityCap = Quality.Legendary, string improves = null)
        {
            Id = RequireId(id);
            Profession = profession;
            Station = RequireStation(station);
            Inputs = RequireList(inputs, nameof(inputs));
            Outputs = RequireList(outputs, nameof(outputs));
            MinStationLevel = RequireStationLevel(minStationLevel);
            RequiredParts = RequireParts(requiredParts);
            MinProfessionLevel = Guard.Positive(minProfessionLevel, nameof(minProfessionLevel));
            CraftSeconds = Guard.NonNegative(craftSeconds, nameof(craftSeconds));
            Xp = Guard.NonNegative(xp, nameof(xp));
            Unlock = unlock ?? UnlockCondition.AlwaysKnown;
            QualityCap = RequireCap(qualityCap);
            Improves = RequireImproves(improves, id);
        }

        /// <summary>True si el jugador conoce la receta según su <see cref="Unlock"/>.</summary>
        public bool IsKnownBy(IRecipeKnowledge knowledge) => Unlock.IsMet(knowledge);

        public override string ToString() =>
            $"{Id} ({Profession} {MinProfessionLevel}, {Station} {MinStationLevel}) → " +
            string.Join(", ", Outputs.Select(o => o.ToString()));
    }
}
