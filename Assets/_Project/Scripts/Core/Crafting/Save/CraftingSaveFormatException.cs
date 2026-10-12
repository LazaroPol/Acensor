using System;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>El texto guardado no es un guardado de oficios válido (GDD §17.7).</summary>
    public sealed class CraftingSaveFormatException : Exception
    {
        public CraftingSaveFormatException(string message) : base(message) { }
    }
}
