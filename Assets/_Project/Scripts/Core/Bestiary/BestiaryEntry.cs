using System;
using Specimen.Core.Combat.Enemies;
using Specimen.Core.Common;

namespace Specimen.Core.Bestiary
{
    /// <summary>Criatura del bestiario: arquetipo de combate + modelo, variante de color (1-3) y escala.</summary>
    public sealed class BestiaryEntry
    {
        public const int Variants = 3;

        public BestiaryEntry(EnemyArchetype archetype, string displayName, MonsterModel model, int variant = 1,
            float scale = 1f)
        {
            Archetype = Guard.NotNull(archetype, nameof(archetype));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Nombre vacío.", nameof(displayName));
            if (variant < 1 || variant > Variants)
                throw new ArgumentOutOfRangeException(nameof(variant), variant, $"Variante entre 1 y {Variants}.");
            DisplayName = displayName;
            Model = model;
            Variant = variant;
            Scale = Guard.Positive(scale, nameof(scale));
        }

        public EnemyArchetype Archetype { get; }
        public string Id => Archetype.Id;
        public string DisplayName { get; }
        public MonsterModel Model { get; }
        public int Variant { get; }
        public float Scale { get; }

        public string ModelName => Model.ToString();
        public string MaterialKey => Model.ToString();
        public string BaseColorTexture => $"T_{Model}_BaseColor_{Variant}";
    }
}
