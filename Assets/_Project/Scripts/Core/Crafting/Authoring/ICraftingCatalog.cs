using System.Collections.Generic;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Punto de entrada único a los datos de oficios ya convertidos al núcleo.</summary>
    public interface ICraftingCatalog
    {
        IReadOnlyList<ItemDefinition> Items { get; }
        RecipeCatalog Recipes { get; }
        IReadOnlyList<KnowledgeDefinition> Knowledge { get; }
        IReadOnlyList<StationPartEntry> Parts { get; }
        IReadOnlyList<ModDefinition> Mods { get; }
        ProfessionCurve Curve { get; }
        ProfessionMilestones Milestones { get; }
        QualityRules Quality { get; }
        bool TryGetItem(string id, out ItemDefinition item);
    }
}
