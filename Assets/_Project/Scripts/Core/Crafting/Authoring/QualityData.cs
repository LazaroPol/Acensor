using System;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Datos serializables de <see cref="QualityRules"/>: pesos de la fórmula y los seis grados.</summary>
    [Serializable]
    public sealed class QualityData
    {
        public int perLevel;
        public int perTier;
        public int perStationLevel;
        public int randomMin;
        public int randomMax;
        public GradeData[] grades = new GradeData[0];
    }

    [Serializable]
    public sealed class GradeData
    {
        public Quality quality;
        public string name;
        public int minScore;
        public int modSlots;
        public float statMultiplier = 1f;
        public float durabilityMultiplier = 1f;
    }
}
