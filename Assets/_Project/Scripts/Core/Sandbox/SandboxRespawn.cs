using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Common;

namespace Specimen.Core.Sandbox
{
    /// <summary>Cuánto tarda en reaparecer cada criatura del Campo de pruebas (el jefe, más).</summary>
    public static class SandboxRespawn
    {
        public const float Common = 8f;
        public const float Boss = 20f;

        public static float DelayFor(BestiaryEntry entry)
            => Guard.NotNull(entry, nameof(entry)).Id == ActOneBoss.Id ? Boss : Common;
    }
}
