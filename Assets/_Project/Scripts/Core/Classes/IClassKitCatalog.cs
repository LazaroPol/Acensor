using System.Collections.Generic;

namespace Specimen.Core.Classes
{
    /// <summary>Fuente de definiciones de kits de clase.</summary>
    public interface IClassKitCatalog
    {
        IReadOnlyList<ClassKit> All { get; }
        ClassKit Get(ClassKitId id);
        IReadOnlyList<ClassKit> ByRole(ClassRole role);
    }
}
