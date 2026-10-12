using System;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Datos serializables de <see cref="ProfessionCurve"/> + <see cref="ProfessionMilestones"/>.</summary>
    [Serializable]
    public sealed class CurveData
    {
        /// <summary>XP para pasar del nivel n al n+1 (índice 0 = nivel 1).</summary>
        public int[] xpToNext = new int[0];
        public MilestoneData[] milestones = new MilestoneData[0];
    }

    [Serializable]
    public sealed class MilestoneData
    {
        public int level = 1;
        public int materialTier = 1;
        public float timeBonus;
        public int maxQualityBonus;
    }
}
