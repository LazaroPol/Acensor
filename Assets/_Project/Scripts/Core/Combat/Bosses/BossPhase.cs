using Specimen.Core.Common;

namespace Specimen.Core.Combat.Bosses
{
    /// <summary>Fase de un jefe: se activa cuando su vida baja hasta cierta fracción y modifica su daño.</summary>
    public sealed class BossPhase
    {
        public BossPhase(string id, float entryHealthFraction, float damageMultiplier)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new System.ArgumentException("El id de la fase no puede estar vacío.", nameof(id));
            if (entryHealthFraction < 0f || entryHealthFraction > 1f)
                throw new System.ArgumentOutOfRangeException(nameof(entryHealthFraction));
            Id = id;
            EntryHealthFraction = entryHealthFraction;
            DamageMultiplier = Guard.Positive(damageMultiplier, nameof(damageMultiplier));
        }

        public string Id { get; }

        /// <summary>Fracción de vida (0-1) a partir de la cual empieza la fase.</summary>
        public float EntryHealthFraction { get; }

        public float DamageMultiplier { get; }
    }
}
