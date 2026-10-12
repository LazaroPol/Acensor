using System;

namespace Specimen.Core.Items
{
    /// <summary>Datos de un grado: nombre visible, umbral de puntos, huecos de mod y multiplicadores.</summary>
    public sealed class QualityGrade
    {
        public QualityGrade(Quality quality, string name, int minScore, int modSlots,
            float statMultiplier, float durabilityMultiplier)
        {
            if (quality == Quality.None) throw new ArgumentException("Grado sin calidad.", nameof(quality));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nombre vacío.", nameof(name));
            if (modSlots < 0) throw new ArgumentOutOfRangeException(nameof(modSlots));
            if (statMultiplier <= 0f) throw new ArgumentOutOfRangeException(nameof(statMultiplier));
            if (durabilityMultiplier <= 0f) throw new ArgumentOutOfRangeException(nameof(durabilityMultiplier));
            Quality = quality;
            Name = name;
            MinScore = minScore;
            ModSlots = modSlots;
            StatMultiplier = statMultiplier;
            DurabilityMultiplier = durabilityMultiplier;
        }

        public Quality Quality { get; }
        public string Name { get; }

        /// <summary>Puntos mínimos de calidad para alcanzar este grado.</summary>
        public int MinScore { get; }
        public int ModSlots { get; }
        public float StatMultiplier { get; }
        public float DurabilityMultiplier { get; }

        /// <summary>Durabilidad máxima escalada por el grado (redondeada, mínimo 1 si la base es positiva).</summary>
        public int ScaleDurability(int baseDurability)
        {
            if (baseDurability <= 0) return baseDurability;
            return Math.Max(1, (int)Math.Round(baseDurability * DurabilityMultiplier, MidpointRounding.AwayFromZero));
        }

        public override string ToString() => $"{Name} ({(int)Quality})";
    }
}
