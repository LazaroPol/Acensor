using System.Collections.Generic;

namespace Specimen.Core.Prototype
{
    /// <summary>Estrategia que describe un nivel como lista de visuales a instanciar.</summary>
    public interface ILevelLayout
    {
        IReadOnlyList<LevelPlacement> Build();
    }
}
