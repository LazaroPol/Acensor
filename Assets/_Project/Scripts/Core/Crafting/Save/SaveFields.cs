using System;
using System.Globalization;

namespace Specimen.Core.Crafting.Save
{
    /// <summary>Conversión de campos de una línea <c>clave=a:b:c</c> con errores de formato claros.</summary>
    internal static class SaveFields
    {
        private static readonly char[] Forbidden = { ':', ',', '=', '\n', '\r' };

        public static string Id(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.IndexOfAny(Forbidden) >= 0)
                throw new CraftingSaveFormatException($"Id no guardable: «{id}».");
            return id;
        }

        public static string Text(int n) => n.ToString(CultureInfo.InvariantCulture);
        public static string Text(double d) => d.ToString("R", CultureInfo.InvariantCulture);

        public static string[] Split(string value, int count, string key)
        {
            var parts = value.Split(':');
            if (parts.Length != count) throw new CraftingSaveFormatException($"«{key}» espera {count} campos: {value}");
            return parts;
        }

        public static string NonEmpty(string field, string key)
        {
            if (field.Length == 0) throw new CraftingSaveFormatException($"Campo vacío en «{key}».");
            return field;
        }

        public static int Int(string field, string key)
        {
            if (!int.TryParse(field, NumberStyles.None, CultureInfo.InvariantCulture, out var n))
                throw new CraftingSaveFormatException($"Número inválido en «{key}»: {field}");
            return n;
        }

        public static double Seconds(string field, string key)
        {
            if (!double.TryParse(field, NumberStyles.Float, CultureInfo.InvariantCulture, out var d) || !(d >= 0) || double.IsInfinity(d))
                throw new CraftingSaveFormatException($"Tiempo inválido en «{key}»: {field}");
            return d;
        }

        public static Items.Quality Quality(string field, string key)
        {
            var n = Int(field, key);
            if (!System.Enum.IsDefined(typeof(Items.Quality), n)) throw new CraftingSaveFormatException($"Calidad inválida: {field}");
            return (Items.Quality)n;
        }

        public static TEnum Enum<TEnum>(string field, string key) where TEnum : struct
        {
            if (field.Length == 0 || char.IsDigit(field[0]) || field[0] == '-' || !System.Enum.TryParse(field, false, out TEnum value))
                throw new CraftingSaveFormatException($"Valor desconocido en «{key}»: {field}");
            return value;
        }
    }
}
