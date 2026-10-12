using System;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Requisitos para instalar un mod: calidad mínima del objeto y, opcionalmente, nivel de oficio.</summary>
    public sealed class ModRequirements
    {
        public static readonly ModRequirements None = new ModRequirements();

        public ModRequirements(Quality minQuality = Quality.None, ProfessionId? profession = null, int minLevel = 0)
        {
            if (minLevel < 0) throw new ArgumentOutOfRangeException(nameof(minLevel));
            if (minLevel > 0 && profession == null)
                throw new ArgumentException("Un nivel mínimo necesita oficio.", nameof(profession));
            MinQuality = minQuality;
            Profession = profession;
            MinLevel = minLevel;
        }

        public Quality MinQuality { get; }
        public ProfessionId? Profession { get; }
        public int MinLevel { get; }

        /// <summary>True si el objeto y el instalador cumplen; sin niveles conocidos, el oficio cuenta como 0.</summary>
        public bool IsMetBy(Quality itemQuality, ProfessionLevels levels)
        {
            if (itemQuality < MinQuality) return false;
            if (Profession == null || MinLevel == 0) return true;
            var level = levels == null ? 0 : levels.LevelOf(Profession.Value);
            return level >= MinLevel;
        }
    }
}
