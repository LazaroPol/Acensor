using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Resuelve las etiquetas de equipo de un objeto base para comprobar compatibilidad de mods.</summary>
    public interface IGearTagProvider
    {
        GearTags TagsOf(ItemDefinition item);
    }
}
