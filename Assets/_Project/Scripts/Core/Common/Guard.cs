using System;

namespace Specimen.Core.Common
{
    /// <summary>Validaciones de precondiciones para fallar pronto con mensajes claros.</summary>
    public static class Guard
    {
        public static T NotNull<T>(T value, string paramName) where T : class
            => value ?? throw new ArgumentNullException(paramName);

        public static float NonNegative(float value, string paramName)
            => value >= 0f ? value : throw new ArgumentOutOfRangeException(paramName, value, "Debe ser >= 0.");

        public static int NonNegative(int value, string paramName)
            => value >= 0 ? value : throw new ArgumentOutOfRangeException(paramName, value, "Debe ser >= 0.");

        public static float Positive(float value, string paramName)
            => value > 0f ? value : throw new ArgumentOutOfRangeException(paramName, value, "Debe ser > 0.");

        public static int Positive(int value, string paramName)
            => value > 0 ? value : throw new ArgumentOutOfRangeException(paramName, value, "Debe ser > 0.");
    }
}
