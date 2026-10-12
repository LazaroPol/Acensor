using System;
using Specimen.Core.Common;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Elección del jugador (desde la UI) de un ítem concreto para un ingrediente por etiqueta:
    /// el planificador lo gasta antes que el orden por defecto (útil para subir la calidad).
    /// </summary>
    public sealed class IngredientChoice
    {
        /// <summary>Índice del ingrediente (en la lista de specs) al que se aplica.</summary>
        public int SpecIndex { get; }
        public string ItemId { get; }

        public IngredientChoice(int specIndex, string itemId)
        {
            SpecIndex = Guard.NonNegative(specIndex, nameof(specIndex));
            if (string.IsNullOrWhiteSpace(itemId)) throw new ArgumentException("Id vacío.", nameof(itemId));
            ItemId = itemId;
        }
    }
}
