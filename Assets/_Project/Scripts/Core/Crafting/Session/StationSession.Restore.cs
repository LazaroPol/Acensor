using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Carga de un guardado (GDD §17.7): nivel, piezas con su desgaste y trabajos en cola.</summary>
    public sealed partial class StationSession
    {
        /// <summary>Sube la estación hasta el nivel guardado aplicando sus mejoras sin coste.</summary>
        public void RestoreLevel(int level)
        {
            while (Station.Level < level && !Station.IsMaxLevel)
            {
                var upgrade = NextUpgrade;
                if (upgrade == null) return;
                Station.Restore(upgrade);
            }
        }

        /// <summary>Coloca una pieza guardada con su durabilidad; false si no se conoce o no cabe.</summary>
        public bool RestorePart(string partId, int durability)
        {
            var entry = _services.PartOf(partId);
            if (entry == null) return false;
            var part = new StationPart(entry.Definition);
            while (part.Durability > durability && part.Durability > 0) part.Wear();
            return Station.PlacePart(part) == PartPlaceResult.Placed;
        }

        /// <summary>Vuelve a encolar un trabajo con sus segundos restantes; false si no cabe.</summary>
        public bool RestoreJob(RecipeDefinition recipe, ProfessionId profession, float remainingSeconds) =>
            Queue.Restore(recipe, Profile.Skill(profession), remainingSeconds, Profile.Bag) != null;
    }
}
