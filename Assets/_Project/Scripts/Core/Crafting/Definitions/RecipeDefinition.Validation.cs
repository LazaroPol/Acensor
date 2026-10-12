using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Validación temprana de <see cref="RecipeDefinition"/>: los datos malos fallan al crearla.</summary>
    public sealed partial class RecipeDefinition
    {
        private static string RequireId(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            return id;
        }

        private static StationType RequireStation(StationType station)
        {
            if (!StationTypes.IsDefined(station))
                throw new ArgumentOutOfRangeException(nameof(station), station, "Estación desconocida.");
            return station;
        }

        private static IReadOnlyList<T> RequireList<T>(IEnumerable<T> items, string paramName) where T : class
        {
            var list = Guard.NotNull(items, paramName).ToList();
            if (list.Count == 0 || list.Any(i => i == null))
                throw new ArgumentException("La lista no puede estar vacía ni tener nulos.", paramName);
            return list.AsReadOnly();
        }

        private static int RequireStationLevel(int level)
        {
            if (level < MinStationLevelValue || level > MaxStationLevelValue)
                throw new ArgumentOutOfRangeException(nameof(level), level, "Nivel de estación fuera de rango.");
            return level;
        }

        private static IReadOnlyList<string> RequireParts(IEnumerable<string> parts)
        {
            var list = (parts ?? Enumerable.Empty<string>()).ToList();
            if (list.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Pieza con id vacío.", nameof(parts));
            if (list.Distinct().Count() != list.Count)
                throw new ArgumentException("Pieza repetida.", nameof(parts));
            return list.AsReadOnly();
        }

        private static Quality RequireCap(Quality cap)
        {
            if (cap < Quality.Crude || cap > Quality.Legendary)
                throw new ArgumentOutOfRangeException(nameof(cap), cap, "Calidad máxima fuera de rango.");
            return cap;
        }

        private static string RequireImproves(string improves, string id)
        {
            if (improves == null) return null;
            if (string.IsNullOrWhiteSpace(improves))
                throw new ArgumentException("Improves vacío.", nameof(improves));
            if (improves == id) throw new ArgumentException("Una receta no puede mejorarse a sí misma.", nameof(improves));
            return improves;
        }
    }
}
