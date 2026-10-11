using System.Collections.Generic;
using Specimen.Core.Professions;

namespace Specimen.Core.Floors.Paths
{
    /// <summary>
    /// Qué profesiones abren una puerta sellada y cómo: Manitas fuerza, Químico disuelve y Escultor restaura
    /// la estatua (GDD §7). El resto de profesiones no la abren.
    /// </summary>
    public static class SealedDoorRules
    {
        private static readonly Dictionary<ProfessionId, SealMethod> Methods = new Dictionary<ProfessionId, SealMethod>
        {
            { ProfessionId.Manitas, SealMethod.Forzar },
            { ProfessionId.Quimico, SealMethod.Disolver },
            { ProfessionId.Escultor, SealMethod.RestaurarEstatua }
        };

        public static IReadOnlyCollection<ProfessionId> Openers => Methods.Keys;

        public static bool TryGetMethod(ProfessionId profession, out SealMethod method) =>
            Methods.TryGetValue(profession, out method);

        public static bool CanOpen(IEnumerable<ProfessionId> party)
        {
            if (party == null) return false;
            foreach (var p in party)
                if (Methods.ContainsKey(p)) return true;
            return false;
        }
    }
}
