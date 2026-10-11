using System;
using System.Globalization;

namespace Specimen.Core.Prototype
{
    /// <summary>Formato de tiempo del HUD de depuración: mm:ss, redondeando hacia arriba (00:01 hasta expirar).</summary>
    public static class HudTime
    {
        public static string Format(float seconds)
        {
            int total = (int)Math.Ceiling(Math.Max(0f, seconds));
            return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}", total / 60, total % 60);
        }
    }
}
