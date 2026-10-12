using System;

namespace Specimen.Core.Profile
{
    /// <summary>El texto guardado no es un perfil válido.</summary>
    public sealed class ProfileFormatException : Exception
    {
        public ProfileFormatException(string message) : base(message) { }
    }
}
