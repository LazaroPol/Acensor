using System;

namespace Specimen.Core.Classes
{
    /// <summary>Rasgos de exploración que concede un kit (combinables).</summary>
    [Flags]
    public enum ExplorationTraits
    {
        None = 0,
        PushBlocks = 1 << 0,
        TrapResistance = 1 << 1,
        Climb = 1 << 2,
        RevealHidden = 1 << 3,
        Glide = 1 << 4,
        DoubleJump = 1 << 5,
        Dash = 1 << 6,
        ShortTeleport = 1 << 7,
        RemoteActivation = 1 << 8
    }
}
