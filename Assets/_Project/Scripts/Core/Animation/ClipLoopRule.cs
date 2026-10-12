using System;

namespace Specimen.Core.Animation
{
    /// <summary>Qué clips importados se reproducen en bucle: los que el pack marca como «_Loop» y las poses de reposo.</summary>
    public static class ClipLoopRule
    {
        public static bool IsLooping(string clipName)
        {
            if (string.IsNullOrWhiteSpace(clipName)) return false;
            string name = clipName.Substring(clipName.LastIndexOf('|') + 1);
            return name.EndsWith("_Loop", StringComparison.OrdinalIgnoreCase)
                || name.IndexOf("_Loop_", StringComparison.OrdinalIgnoreCase) >= 0
                || name.Equals("Sword_Idle", StringComparison.OrdinalIgnoreCase);
        }
    }
}
