using System.Collections.Generic;

namespace Specimen.Editor.Arte
{
    /// <summary>Orden de los pasos del generador (los controladores de animación van antes, en ArtContext).</summary>
    public static class ArtSteps
    {
        public static IReadOnlyList<IArtStep> All => new IArtStep[]
        {
            new CharacterStep()
        };

        public static IReadOnlyList<ICharacterDecorator> CharacterDecorators => new ICharacterDecorator[]
        {
        };
    }
}
