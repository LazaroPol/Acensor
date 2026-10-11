using System;

namespace Specimen.Core.Items
{
    /// <summary>Desgaste de un objeto. Max = 0 significa indestructible.</summary>
    public interface IDurability
    {
        int Current { get; }
        int Max { get; }
        bool IsBroken { get; }
        bool IsIndestructible { get; }
        float Ratio { get; }

        event Action<int> Changed;
        event Action Broken;

        /// <summary>Resta durabilidad; devuelve la realmente perdida.</summary>
        int Wear(int amount);

        /// <summary>Recupera durabilidad hasta el máximo; devuelve la realmente recuperada.</summary>
        int Repair(int amount);
    }
}
